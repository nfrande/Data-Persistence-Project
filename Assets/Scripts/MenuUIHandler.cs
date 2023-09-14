using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    // public string playerName;
    public GameObject inputField;

    public Text highScoreText;
    
    
    // Start is called before the first frame update
    void Start()
    {
      //  playerName.onEndEdit.AddListener(delegate { SubmitName(playerName);});
          //highScoreText.text = $"High Score : {highScoreName}: {highScore}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClicked()
    {
      GameManager.Instance.playerName = inputField.GetComponent<TMPro.TextMeshProUGUI>().text;
      SceneManager.LoadScene(1);
      Debug.Log(GameManager.Instance.playerName);
    }

    public void SubmitName(InputField userInput)
    {
     //   playerName = string.Parse(userInput.text);
     //   Debug.Log(userInput.text);
    }

    public void Quit()
    {
      #if UNITY_EDITOR
      EditorApplication.ExitPlaymode();
      #else
      Application.Quit();
      #endif
    }
}
