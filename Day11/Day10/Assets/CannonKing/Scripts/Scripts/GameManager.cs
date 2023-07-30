using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    public GameObject menu;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverMenu;
    private int score;
    private int lives;
    public bool isGameOver;
    public float spawnRate = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();

    }


    public void UpdateScore(int pointValue)
    {
        score += pointValue;
        scoreText.SetText("Score : " + score);
    }
    public void UpdateLive(int heart)
    {
        lives += heart;
        livesText.SetText("Lives : " + lives);
        if (lives < 1)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverMenu.gameObject.SetActive(true);
    }

    public void StartGame(int difficulty)
    {
        isGameOver = false;
        score = 0;
        UpdateScore(0);
        UpdateLive(3);
        spawnRate /= difficulty;
        menu.gameObject.SetActive(false);
        StartCoroutine(SpawnTarget());
    }
    IEnumerator SpawnTarget()
    {
        while (isGameOver == false)
        {
            yield return new WaitForSeconds(spawnRate);
            spawnManager.SpawnEnemy();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
