using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameExitPanel;

    private bool _hasAudioPlayed;
    private bool _isPlayerAtExit;
    private bool _isPlayerCaught;
    private float _timer;
    private readonly int _timeOffset = 5;
    private Vector3 _iPlayerPosition;
    private Quaternion _iPlayerRotation;
    
    /// <summary>
    /// Method Awake [Life cycle]
    /// </summary>
    void Awake()
    {
        // Manage camvas panels
        menuPanel.SetActive(true);
        pausePanel.SetActive(false);
        gameExitPanel.SetActive(false);
        _iPlayerPosition = player.GetComponent<Transform>().transform.position;
        _iPlayerRotation = player.GetComponent<Transform>().transform.rotation;
    }
    
    /// <summary>
    /// Method Start [Life cycles]
    /// </summary>
    private void Start()
    {
        playableDirector.Stop();
    }
    
    /// <summary>
    /// Method Update [Life cycle]
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
        if (_isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false, winAudio);
        }
        else if (_isPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup,true, caughtAudio);
        }
        
        // Manage Pause mode
        if (Input.GetKeyDown(KeyCode.P) && (gameState != GameState.InMenu))
        {
            gameState = gameState == GameState.InPaused ? GameState.InGame : GameState.InPaused;
            ManagePauseMode();
        }
        
        // Manage return to menu in Game mode
        if (Input.GetKeyDown(KeyCode.Escape) && gameState == GameState.InGame)
        {
            gameState = GameState.InEsc;
            ManageExitToMenu();
        }
    }
    
    /// <summary>
    /// Method StartGameWithIntro
    /// This method call the InitializateGame method passing as parameter a zero value. This value is a initial time to
    /// start a director of Time line director.
    /// </summary>
    public void StartGameWithIntro()
    {
        InitializeGame(0);
    }
    
    /// <summary>
    /// Method StartGame
    /// This method call the InitializateGame passing as parameter a diference between the total duration minus a litle
    /// time offeset to start at end of duration
    /// </summary>
    public void StartGame()
    {
        InitializeGame(playableDirector.duration - _timeOffset);
    }
    
    /// <summary>
    /// Method ManageYesButton
    /// This method manages a "Yes" button in a retrun to menu panel
    /// </summary>
    public void ManageYesButton()
    {
        // Change the game state, manage pause mode and finally reload the scene to set all ghost in the start positions
        gameState = GameState.InGame;
        ManagePauseMode();
        ReloadSceneToMainMenu();
    }
    
    /// <summary>
    /// Method ManageNoButton
    /// This method manages a "No" button in a retrun to menu panel
    /// </summary>
    public void ManageNoButton()
    {
        // Desactivate panel and canvas, change the game state and quit the pause mode
        canvasMenu.SetActive(false);
        gameExitPanel.SetActive(false);
        gameState = GameState.InGame;
        ManagePauseMode();
    }
    
    /// <summary>
    /// Method ExitGame
    /// This method close the aplication
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// Method CaughtPlayer
    /// This method change the property value to true when plsyer is caught 
    /// </summary>
    public void CaughtPlayer()
    {
        _isPlayerCaught = true;
    }
    
    /// <summary>
    /// Method InitializeGame
    /// This method initializate the game on base the initial time to start play on playable director
    /// </summary>
    /// <param name="iTime">double with the initial time to start the director</param>
    private void InitializeGame(double iTime)
    {
        ManageMainMenu(false);
        playableDirector.initialTime = iTime;
        playableDirector.Play();

        gameState = GameState.InGame;
    }

    /// <summary>
    /// Method ManageMainMenu
    /// This method activates the menu panel
    /// </summary>
    /// <param name="isShowingMenu"></param>
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
    
    /// <summary>
    /// Method ManageExitToMenu
    /// This method show the exit to main menu panel when player press "esc" in game mode
    /// </summary>
    private void ManageExitToMenu()
    {
        ManagePauseMode();
        gameExitPanel.SetActive(true);
    }
    
    /// <summary>
    /// Trigger OnTriggerEnter
    /// </summary>
    /// <param name="other">Collider gameObject</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            _isPlayerAtExit = true;
        }
    }
    
    /// <summary>
    /// Method EndLevel
    /// Manages the player caught and win.In any case, we reload the scene because it is easier than resetting all the
    /// variables, ghosts, panels... etc.
    /// </summary>
    /// <param name="canvas"></param>
    /// <param name="doRestart"></param>
    /// <param name="audioSource"></param>
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
            /*if (doRestart) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            else Application.Quit();
            */
            
            // Manage reload
            if (doRestart)
            {
                // Reset game state
                _isPlayerCaught = false;
                _hasAudioPlayed = false;

                player.transform.position = _iPlayerPosition;
                player.transform.rotation = _iPlayerRotation;
                canvas.alpha = 0;
                _timer = 0;
                return;
            }
            
            ReloadSceneToMainMenu();
        }
    }
    
    /// <summary>
    /// Method ReloadScene
    /// This method reload the current active scene
    /// </summary>
    private void ReloadSceneToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
