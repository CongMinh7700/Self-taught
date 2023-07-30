using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;
    public Button resumeButton;
    private float timeRate = 1.0f;
    private int score;
    public bool isGameActive;
    private int lives ;
    public bool isLive ;
  
    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive )
        {
            isGameActive = false;
            Pause();
        }
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isLive = true;
        timeRate /= difficulty;
        score = 0;
        lives = 3;
        StartCoroutine(SpawnTarget());    
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(timeRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            
        }
    }
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score : " + score;
    }
    public void UpdateLives(int value)
    {
        lives += value;
        livesText.text = "Lives : " + lives;
        if (lives < 1)
        {
            isLive = false;
            GameOver();
        }
    }

    public void GameOver()
    {
       
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        resumeButton.gameObject.SetActive(true);
    }
    public void Continue()
    {
        isGameActive = true;
        resumeButton.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
