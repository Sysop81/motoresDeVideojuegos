using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private  TextMeshProUGUI scoreText;
    private const int MIN_POINT_VALUE = 1;
    private const int MAX_POINT_VALUE = 3;
    private int _score;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        UpdateScore();
    }
    
    /// <summary>
    /// Getter GetMaxScorePointValue
    /// </summary>
    /// <returns>int</returns>
    public int GetMaxScorePointValue()
    {
        return MAX_POINT_VALUE;
    }
    
    /// <summary>
    /// Getter GetMinScorePointValue
    /// </summary>
    /// <returns>int</returns>
    public int GetMinScorePointValue()
    {
        return MIN_POINT_VALUE;
    }
    
    /// <summary>
    /// Setter SetScore
    /// </summary>
    /// <param name="score">Value to sum</param>
    public void SetScore(int score)
    {
        _score += score;
        UpdateScore();
    }
    
    /// <summary>
    /// Method UpdateScore
    /// This method updates the Score GUI Text 
    /// </summary>
    private void UpdateScore()
    {
        scoreText.text = $"Score: {_score} pts";
    }
}
