using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_Text nameText;
    public TMP_Text displayNameText;
    // Start is called before the first frame update
    private void Start()
    {
        nameInputField = GameObject.Find("Name Input").GetComponent<TMP_InputField>();
        nameText = GameObject.Find("Name Text").GetComponent<TMP_Text>();
        LoadPlayerName();
    }
    public void OnValueChanged()
    {
       
        nameText.text = nameInputField.text;
        
       
    }
    public void StartNew()
    {
        SavePlayerName();
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

        EditorApplication.ExitPlaymode();
        //Application.Quit();
       
    }
    public void SavePlayerName()
    {
      
        string playerName = nameInputField.text;
        PlayerPrefs.SetString("PlayerName",playerName);
        PlayerPrefs.Save();
    }
    public void LoadPlayerName()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultName");
        string highScorePlayerName = PlayerPrefs.GetString("HighScorePlayerName", "DefaultName");
        nameInputField.text = playerName;
        nameText.text = highScorePlayerName;
        displayNameText.text = "Best Score : " + highScorePlayerName;
    }
}
