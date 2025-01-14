using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRB;
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float forwardInput;
    [SerializeField] private float restartGamePlayTime = 3f;
    [SerializeField] private float powerUpForce = 20f;
    [SerializeField] private GameObject[] powerUpRings;
    
    private GameObject _focalPoint;
    private bool _hasPowerUp;
    private float _powerUpTime = 7f;
    private const string STAR_POWER_UP = "Star_01(Clone)";
    private GameObject _spawnManager;
    private GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        _playerRB.AddForce(forwardInput * moveForce * _focalPoint.transform.forward, ForceMode.Force);
    }
    
    /// <summary>
    /// Getter HasPowerUp
    /// </summary>
    /// <returns></returns>
    public bool HasPowerUp()
    {
        return _hasPowerUp;
    }

    /// <summary>
    /// Method OnTriggerEnter [Trigger]
    /// </summary>
    /// <param name="other">GameObject detected</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            Invoke("RestartGame",restartGamePlayTime);
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            // Check if powerUp is a Star (Destroy all anemies)
            if (other.gameObject.name.Equals(STAR_POWER_UP))
            {
                // Call method and set function return
                DestroyAllEnemies();
                return;
            }
            _hasPowerUp = true;
            StartCoroutine(PowerUpCountDown());
        }
    }
    
    /// <summary>
    /// Method OnCollisionEnter [Callback]
    /// This method start when this gameObject collision with other gameObject.
    /// </summary>
    /// <param name="other">GameObject detected</param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && _hasPowerUp)
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            otherRB.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// Method PowerUpCountDown [Corrutine]
    /// This method manages the standar powerUp countDown
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerUpCountDown()
    {
        for (int i = 0; i < powerUpRings.Length; i++)
        {
            powerUpRings[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(_powerUpTime / powerUpRings.Length);
            powerUpRings[i].SetActive(false);
        }
        _hasPowerUp = false;
    }
    
    /// <summary>
    /// Method DestroyAllEnemies
    /// This method destroy all enemies and launch a new enemy wave calling to spawnManager
    /// </summary>
    private void DestroyAllEnemies()
    {
        // Get all active enemies
        var enemies = GameObject.FindObjectsOfType<EnemyController>();
        var totalPoints = enemies.Length * _gameManager.GetMaxScorePointValue(); // It with powerUp On
        // Destroy all of these
        foreach (var enemy in enemies)
            Destroy(enemy.gameObject);
        
        // Update Score 
        _gameManager.SetScore(totalPoints);
        
        // Call the LaunchEnemyWave to load a new enemy wave from spawn manager.
        _spawnManager.GetComponent<SpawnManager>().LaunchNewEnemyWave();
    }
    
    /// <summary>
    /// Method RestartGame
    /// This method restart game
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
