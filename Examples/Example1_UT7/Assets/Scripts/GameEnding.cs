using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum GameState
{
    InMenu,
    InGame,
    InPaused,
    InEsc
}

public class GameEnding : MonoBehaviour
{
    public GameState gameState = GameState.InMenu;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float displayImageDuration = 1f;
    [SerializeField] private GameObject player;
    [SerializeField] private CanvasGroup exitBackgroundImageCanvasGroup;
    [SerializeField] private CanvasGroup caughtBackgroundImageCanvasGroup;
    [SerializeField] private AudioSource caughtAudio;
    [SerializeField] private AudioSource winAudio;
    private bool _hasAudioPlayed;
    private bool _isPlayerAtExit;
    private bool _isPlayerCaught;
    private float _timer;
    
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private PlayableDirector playableDirector;
    private readonly int _timeOffset = 5;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameExitPanel;

    
    
    void Awake()
    {
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameExitPanel.SetActive(false);
    }
    
    private void Start()
    {
        playableDirector.Stop();
    }

    public void StartGameWithIntro()
    {
        InitializeGame(0);
    }

    public void StartGame()
    {
        InitializeGame(playableDirector.duration - _timeOffset);
    }

    public void ManageYesButton()
    {
        gameState = GameState.InGame;
        ManagePauseMode();
        ReloadScene();
    }

    public void ManageNoButton()
    {
        canvasMenu.SetActive(false);
        gameExitPanel.SetActive(false);
        gameState = GameState.InGame;
        ManagePauseMode();
    }

    private void InitializeGame(double iTime)
    {
        ManageMainMenu(false);
        playableDirector.initialTime = iTime;
        playableDirector.Play();

        gameState = GameState.InGame;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ManageMainMenu(bool isShowingMenu)
    {
        canvasMenu.SetActive(isShowingMenu);
        menuPanel.SetActive(isShowingMenu);
    }
    
    /// <summary>
    /// Method ManagePauseMode
    /// This method manage game pause and game resume
    /// </summary>
    private void ManagePauseMode()
    {
        if (gameState == GameState.InPaused || gameState == GameState.InEsc)
        {
            Time.timeScale = 0.0f;
            canvasMenu.SetActive(true);
            if(gameState == GameState.InPaused) pausePanel.SetActive(true);
            return;
        }
        canvasMenu.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,true, winAudio);
        }
        else if (_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true, caughtAudio);
        }

        if (Input.GetKeyDown(KeyCode.P) && (gameState != GameState.InMenu))
        {
            gameState = gameState == GameState.InPaused ? GameState.InGame : GameState.InPaused;
            ManagePauseMode();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.InGame)
        {
            gameState = GameState.InEsc;
            ManageExitToMenu();
        }
    }

    private void ManageExitToMenu()
    {
        ManagePauseMode();
        gameExitPanel.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            _isPlayerAtExit = true;
        }
    }

    private void EndLevel(CanvasGroup canvas, bool doRestart, AudioSource audioSource)
    {
        if (!_hasAudioPlayed)
        {
            audioSource.Play();
            _hasAudioPlayed = true;
        }
        
        _timer += Time.deltaTime;
        canvas.alpha = _timer / fadeDuration;
        if (_timer > fadeDuration + displayImageDuration)
        {
            if (doRestart) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else Application.Quit();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void CaughtPlayer()
    {
        _isPlayerCaught = true;
    }
}
