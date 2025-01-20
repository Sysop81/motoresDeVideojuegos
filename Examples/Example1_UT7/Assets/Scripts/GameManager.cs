using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    InIntro,
    InGame,
    GameOver
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject timeLineDirector;
    [SerializeField] private GameObject ambientSound;
    [SerializeField] private Button startWithIntro;
    [SerializeField] private Button startWithoutIntro;
    [SerializeField] private Button exitGame;
    
    
    //public GameState gameState = GameState.In;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
