using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField][Range(1,3)] private int difficulty;
    private Button _startBtn;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _startBtn = GetComponent<Button>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _startBtn.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }
}
