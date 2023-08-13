using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    private int _score;
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject _playButton;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Pause();
    }
    private void Start()
    {
        _gameOver.gameObject.SetActive(false);
    }
    public void UpdateScore()
    {
        _score++;
        _scoreText.text = _score.ToString();
        Debug.Log(_score);
    }
    public void GameOver()
    {
        _gameOver.gameObject.SetActive(true);
        _playButton.gameObject.SetActive(true);
        Pause();
    }
    public void Play()
    {
        Time.timeScale = 1;
        _score = 0;
        _scoreText.text = _score.ToString();
        _playButton.gameObject.SetActive(false);
        _gameOver.gameObject.SetActive(false);
        player.enabled = true;
        DestroyPipes();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        player.enabled = false;

    }
    private void DestroyPipes()
    {
        Pipes[] pipes = FindObjectsOfType<Pipes>();
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }
}
