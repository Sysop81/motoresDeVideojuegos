using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    private GameEnding _gameEnding;
    private AudioSource _audioSource;
    
    
    /// <summary>
    /// Method Start [Life cycle]
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _gameEnding = GameObject.Find("GameEnding").GetComponent<GameEnding>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
    }

    /// <summary>
    /// Method Update [Life cycle]
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Stop audio when game mode is in paused or in Esc [Question to return to menu] mode
        if (_gameEnding.gameState == GameState.InPaused || _gameEnding.gameState == GameState.InEsc)
        {
            _audioSource.Stop();
            return;
        }
        
        // If is in game mode and adio is not playing
        if(!_audioSource.isPlaying && _gameEnding.gameState == GameState.InGame) 
            _audioSource.Play();
    }
}
