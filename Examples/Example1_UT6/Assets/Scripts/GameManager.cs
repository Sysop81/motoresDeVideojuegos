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
    InPaused,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public GameState gameState = GameState.Loading;
    [SerializeField] private GameObject livesContainer;
    [SerializeField] private List<GameObject> lives;
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float spawnRate = 1.0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private GameObject conffeti;
    private int _score;
    private int _maxScore;
    private const string MAX_SCORE = "MAX_SCORE";
    private int _numLives = 3;
    
    /// <summary>
    /// Method Start [Life cycles]
    /// Start is called before the first frame update
    /// </summary>
    private void Start()
    {
        livesContainer.SetActive(false);
        conffeti.gameObject.SetActive(false);
        ShowMaxScore();
    }
    
    /// <summary>
    /// Method Update [Life cycles]
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        // Manage pause mode
        if (Input.GetKeyUp(KeyCode.P) && (gameState != GameState.Loading && gameState != GameState.GameOver))
        {
            gameState = gameState == GameState.InPaused ? GameState.InGame : GameState.InPaused;
            ManagePauseMode();
        }
    }
    
    /// <summary>
    /// Method ManagePauseMode
    /// This method manage game pause and game resume
    /// </summary>
    private void ManagePauseMode()
    {
        if (gameState == GameState.InPaused)
        {
            Time.timeScale = 0.0f;
            pauseText.gameObject.SetActive(true);
            return;
        }
        
        Time.timeScale = 1.0f;
        pauseText.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// IEnumerator SpawnTarget
    /// Manages the spawn target
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnTarget()
    {
        while (gameState == GameState.InGame)
        {
            yield return new WaitForSeconds(spawnRate);
            var index = Random.Range(0, targets.Count);
            Instantiate(targets[index],transform.position,targets[index].transform.rotation);
        }
    }
    
    /// <summary>
    /// Method UpdateScore
    /// This method updates the score text
    /// </summary>
    /// <param name="value"></param>
    public void UpdateScore(int value)
    {
        _score += value;
        scoreText.text = "Score: " + _score;
    }
    
    /// <summary>
    /// Method GameOver
    /// This method manages the counter lives and game state
    /// </summary>
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
    
    /// <summary>
    /// Method UpdatePanelLives
    /// This method updates the live panel
    /// </summary>
    private void UpdatePanelLives()
    {
        Image heratImage = lives[_numLives].GetComponent<Image>();
        var tempColor = heratImage.color;
        tempColor.a = 0.3f;
        heratImage.color = tempColor;
    }
    
    /// <summary>
    /// Method RestartGame
    /// This method reload the game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    /// <summary>
    /// Method StartGame
    /// This method manages the start of the game
    /// </summary>
    /// <param name="difficulty"></param>
    public void StartGame(int difficulty)
    {
        spawnRate = spawnRate / difficulty;
        gameState = GameState.InGame;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        livesContainer.SetActive(true);
        conffeti.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        initialMenu.gameObject.SetActive(false);
        ManageLivesByDifficulty(difficulty);

        //PlayerPrefs.DeleteKey(MAX_SCORE);
    }
    
    /// <summary>
    /// Method ManageLivesByDifficulty
    /// This method builds the life panel based on the selected difficulty
    /// </summary>
    /// <param name="difficulty"></param>
    private void ManageLivesByDifficulty(int difficulty)
    {
        int[] aLives = { 3, 2, 1 };
        _numLives = aLives[difficulty - 1];
        
        if (_numLives == 3) return;
        
        var livesToDesactivate = lives.Count - _numLives;
        var counter = 0;
        for (int i = lives.Count - 1; i >= 0; i--)
        {
            if (counter >= livesToDesactivate) break;
            lives[i].SetActive(false);
            counter++;
        }
    }
    
    /// <summary>
    /// Method ShowMaxScore
    /// This method shows the max score to user
    /// </summary>
    private void ShowMaxScore()
    {
        _maxScore = PlayerPrefs.GetInt(MAX_SCORE,0);
        scoreText.text = "Max Score: " + _maxScore;
    }
    
    /// <summary>
    /// Method SetMaxScore
    /// This method updates the max score registered and launch a conffi celebration particle system
    /// </summary>
    private void SetMaxScore()
    {
        if (_score > _maxScore)
        {
            PlayerPrefs.SetInt(MAX_SCORE,_score);
            StartCoroutine(LaunchConfettiCelebration());
        }
    }
    
    /// <summary>
    /// IEnumerator LaunchConfettiCelebration
    /// Corrutine to manage a conffeti particle system
    /// </summary>
    /// <returns></returns>
    IEnumerator LaunchConfettiCelebration()
    {
        scoreText.text = "New record: " + _score;
        conffeti.gameObject.SetActive(true);
        yield return new WaitForSeconds(4.0f); // 4 Seconds
        conffeti.gameObject.SetActive(false);
    }
}
