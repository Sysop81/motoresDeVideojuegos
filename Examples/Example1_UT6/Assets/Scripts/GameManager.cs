using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum GameState
{
    Loading,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Loading;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject initialMenu;
    private int _score;
    private int _maxScore;
    private const string MAX_SCORE = "MAX_SCORE";
    private int _numLives = 3;
    [SerializeField] private List<GameObject> lives;
    private void Start()
    {
        ShowMaxScore();
    }

    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.InGame)
        {
            yield return new WaitForSeconds(spawnRate);
            var index = Random.Range(0, targets.Count);
            Instantiate(targets[index],transform.position,targets[index].transform.rotation);
        }
    }

    public void UpdateSocre(int value)
    {
        _score += value;
        scoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        _numLives--;

        if (_numLives < 0)
        {
            SetMaxScore();
            gameState = GameState.GameOver;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            UpdatePanelLives();
        }
    }

    private void UpdatePanelLives()
    {
        Image heratImage = lives[_numLives].GetComponent<Image>();
        var tempColor = heratImage.color;
        tempColor.a = 0.3f;
        heratImage.color = tempColor;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(int difficulty)
    {
        spawnRate = spawnRate / difficulty;
        gameState = GameState.InGame;
        StartCoroutine(SpawnTarget());
        UpdateSocre(0);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        initialMenu.gameObject.SetActive(false);
    }
    
    public void ShowMaxScore()
    {
        _maxScore = PlayerPrefs.GetInt(MAX_SCORE,0);
        scoreText.text = "Max Score: " + _maxScore;
    }

    public void SetMaxScore()
    {
        if (_score > _maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE,_score);
        }
    }
}
