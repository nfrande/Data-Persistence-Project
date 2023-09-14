using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;

    public Text playerText;

    public Text highScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    private string highScoreName;

    private int highScore;

    public GameObject gotBricks;

    public AudioClip brickSound;
    public AudioClip explosion;
    public AudioClip levelUp;

    AudioSource audioSource;

    
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnBricks();

        playerText.text = $"Player: {GameManager.Instance.playerName}";

        LoadHighScore();

        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("title");
            }
        }

        gotBricks = GameObject.FindWithTag("Brick");

        if(gotBricks == null && m_Started)
        {
        SpawnBricks();
        GameManager.Instance.maxSpeed += 0.5f;
        audioSource.PlayOneShot(levelUp, 1.0f);
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
        audioSource.PlayOneShot(brickSound, 1.0f);
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        audioSource.PlayOneShot(explosion, 1.0f);

        if(m_Points > highScore)
        {
            SaveHighscore();
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int highScore;
    }

    public void SaveHighscore()
    {
        SaveData data = new SaveData();
        data.playerName = GameManager.Instance.playerName;
        data.highScore = m_Points;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreName = data.playerName;
            highScore = data.highScore;
        }

        highScoreText.text = $"High Score : {highScoreName}: {highScore}";
    }

    public void SpawnBricks()
    {
         const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
}
