using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    [SerializeField][Range(1,3)] private int difficulty;
    private Button _startBtn;
    private GameManager _gameManager;
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _startBtn = GetComponent<Button>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _startBtn.onClick.AddListener(SetDifficulty);
    }
    
    /// <summary>
    /// Method SetDifficulty
    /// </summary>
    private void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
    }
}
