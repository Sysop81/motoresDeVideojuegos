using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class MoveForward : MonoBehaviour
{

    [SerializeField] private float speed = 2.0f; 
    private bool _isEndGame;
    private GameObject _bowlingManager;
    private const float Y_LIMIT = 10.0f;

    void Start()
    {
        // Get the empty bowling container
        _bowlingManager = GameObject.FindWithTag("Bolos");
    }

    // Update is called once per frame
    void Update()
    {
        // Check End Game
        if (_isEndGame && IsBallOutOfTrack())
        {
            // Call "CountBowling" method asigned to the empty bowling container to get the bowling counter";
            Debug.Log("¡¡¡ Game Over !!!  -->  Standing bowlings : " + 
                      (_bowlingManager.GetComponent<BowlingManager>()).CountBowling());
            
            // Destroy this game object
            Destroy(gameObject);
            
            // Stop game
            Time.timeScale = 0f;
        }

        // Ball move
        transform.Translate( speed * Time.deltaTime * Vector3.back);
    }
    
    /**
     * Method IsBallOutOfTrack
     * This method determines if ball gameObject has fallen
     */
    private bool IsBallOutOfTrack()
    {
        return transform.position.y < -10.0f;
    }
    
    /**
     * Method OnTriggerEnter [Handler]
     */
    private void OnTriggerEnter(Collider other)
    {
        // Check collision with "Bolo tag" gameObject to set the flag a true.
        if (other.CompareTag("Bolo"))
        {
            _isEndGame = true;
        }
    }
}
