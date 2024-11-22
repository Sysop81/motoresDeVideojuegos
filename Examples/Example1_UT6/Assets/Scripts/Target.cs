using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private int pointValue;
    [SerializeField] private ParticleSystem explosion;
    private Rigidbody _rb;
    private float minForce = 14,
                  maxForce = 18,
                  maxTorque = 10,
                  xRange = 4,
                  ySpawnPos = -6;
    private GameManager _gameManager;
    //private AudioSource _audioSource; This option is with audio source component
    [SerializeField] private AudioClip _cutSound;
    
    // Start is called before the first frame update
    void Start()
    {
        //_audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(),RandomTorque(),RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minForce, maxForce);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseOver()
    {
        if (_gameManager.gameState == GameState.InGame)
        {
            _gameManager.UpdateSocre(pointValue);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(_cutSound, transform.position,1);
            //_audioSource.Play();
            Instantiate(explosion,transform.position,explosion.transform.rotation);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone"))
        {
            Destroy(gameObject);
            if(this.CompareTag("Good")) _gameManager.GameOver();
        }
    }
}
