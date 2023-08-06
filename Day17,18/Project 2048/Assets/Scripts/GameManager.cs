using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI scoreText;
    public int score;
    private void Start()
    {
        NewGame();
    }
    
    public void NewGame()
    {
        SetScore(0);
        highScoreText.text = LoadHighScore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }
    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver,1f,1f));
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
