using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private AudioClip _cutSound;
    [SerializeField] private int pointValue;
    [SerializeField] private ParticleSystem explosion;
    
    private GameManager _gameManager;
    private Rigidbody _rb;
    private float minForce = 14,
                  maxForce = 18,
                  maxTorque = 10,
                  xRange = 4,
                  ySpawnPos = -6;
    
    
    /// <summary>
    /// Method Start
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }
    
    /// <summary>
    /// Method RandomForce
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }
    
    /// <summary>
    /// Method RandomTorque
    /// </summary>
    /// <returns></returns>
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    
    /// <summary>
    /// Method RandomSpawnPos
    /// </summary>
    /// <returns></returns>
    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    
    /// <summary>
    /// Method OnMouseOver
    /// </summary>
    private void OnMouseOver()
    {
        if (_gameManager.gameState == GameState.InGame)
        {
            _gameManager.UpdateScore(pointValue);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(_cutSound, transform.position,1);
            Instantiate(explosion,transform.position,explosion.transform.rotation);
        }
        
    }
    
    /// <summary>
    /// Trigger OnTriggerEnter
    /// </summary>
    /// <param name="other">Object that triggers the collision</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if(CompareTag("Good")) _gameManager.GameOver();
        }
    }
}
