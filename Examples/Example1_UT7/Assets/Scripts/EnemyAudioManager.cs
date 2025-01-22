using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    private GameEnding _gameEnding;
    private AudioSource _audioSource;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameEnding = GameObject.Find("GameEnding").GetComponent<GameEnding>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameEnding.gameState == GameState.InPaused || _gameEnding.gameState == GameState.InEsc)
        {
            _audioSource.Stop();
            return;
        }
        
        if(!_audioSource.isPlaying && _gameEnding.gameState == GameState.InGame) 
            _audioSource.Play();
    }
}
