using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuUIHandler : MonoBehaviour
{
    // public string playerName;
    public GameObject inputField;
    
    
    // Start is called before the first frame update
    void Start()
    {
      //  playerName.onEndEdit.AddListener(delegate { SubmitName(playerName);});
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
}
