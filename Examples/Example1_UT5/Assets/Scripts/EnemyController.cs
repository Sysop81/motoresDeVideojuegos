using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveForce = 1.5f;
    
    private Rigidbody _rb;
    private GameObject _player;
    private GameObject _spawnManager;
    private GameManager _gameManager;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController = _player.GetComponent<PlayerController>();
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction  = (_player.transform.position - transform.position).normalized;
        _rb.AddForce(direction * moveForce);
    }
    
    /// <summary>
    /// Method OnTriggerEnter [Trigger]
    /// </summary>
    /// <param name="other">GameObject detected</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            // Update score and destroy the enemy gameObject
            _gameManager.SetScore(_playerController.HasPowerUp() ? 
                _gameManager.GetMinScorePointValue() : _gameManager.GetMaxScorePointValue());
            Destroy(gameObject);
            
            // Call LaunchNewEnemy method from _spawnManager GameObject
            if(GameObject.FindObjectsOfType<EnemyController>().Length - 1 == 0)
                _spawnManager.GetComponent<SpawnManager>().LaunchNewEnemyWave();
        }
    }
}
