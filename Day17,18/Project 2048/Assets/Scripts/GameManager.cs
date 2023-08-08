using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private CanvasGroup pause;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score;
    //
    [SerializeField] private GameObject titleGame;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private GameObject tutorialScreen;
    [SerializeField] private Button btnPause;
    [SerializeField] private Button btnExit;


    private void Start()
    {
        titleGame.gameObject.SetActive(true);
        inGameScreen.gameObject.SetActive(false);
    }
    //Restart
    public void NewGame()
    {
        pause.interactable = false;
        pause.alpha = 0f;
        pause.gameObject.SetActive(false);

        gameOver.alpha = 0f;
        gameOver.interactable = false;

        titleScreen.gameObject.SetActive(false);
        inGameScreen.gameObject.SetActive(true);
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

        btnExit.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(true);

    }
    public void GameOver()
    {
        board.enabled = false;
        pause.gameObject.SetActive(false);
        pause.interactable = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f));
        btnPause.gameObject.SetActive(false);
    }   
    public void Pause()
    {
       
        board.enabled = false;
        gameOver.interactable = false;
        pause.interactable = true;
        pause.gameObject.SetActive(true);
        StartCoroutine(Fade(pause, 1f, 0.5f));
        btnExit.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(false);
       

    }
    public void Resume()
    {
        pause.gameObject.SetActive(false);
        pause.alpha = 0f;
        pause.interactable = false;
        board.enabled = true;     
        btnExit.gameObject.SetActive(true);
        btnPause.gameObject.SetActive(true);
      
    }
    public void Tutorial()
    {
        titleScreen.gameObject.SetActive(false);
        tutorialScreen.gameObject.SetActive(true);
    }
    public void ExitTutorial()
    {
        titleScreen.gameObject.SetActive(true);
        tutorialScreen.gameObject.SetActive(false);
    }
    public void ExitMenu()
    {
        EditorApplication.ExitPlaymode();
    }
    public void ExitPause()
    {
        titleScreen.gameObject.SetActive(true);
        inGameScreen.gameObject.SetActive(false);
    }
    private IEnumerator Fade(CanvasGroup canvas,float to,float delay)
    {
        yield return new WaitForSeconds(delay);
        float elapsed = 0;
        float duration = 0.5f;
        float from = canvas.alpha;
        while (elapsed < duration)
        {
            canvas.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvas.alpha = to;
    }
    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }  
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SavedHighScore();
    }
    private void SavedHighScore()
    {
        int highScore = LoadHighScore();
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
