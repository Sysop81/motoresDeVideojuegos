using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private GameObject _player;
    private Vector3 _offset;

    private GameObject _finish;
    private float _limitSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, 5, 20);
        _player = GameObject.FindGameObjectWithTag("Player");
        
        _finish = GameObject.FindGameObjectWithTag("Finish");
        _limitSpawn = _finish.GetComponent<Transform>().position.z - (float)(_finish.GetComponent<Transform>().position.z * 0.8);
        
        // Call the method 'SpawnRandomObstacle' every two seconds
        InvokeRepeating("SpawnRandomObstacle",0,2f);
    }
    
    // Method SpawnRandomObstacle
    void SpawnRandomObstacle()
    {
        // Player position + offset to set obstacle position
        transform.position = _player.transform.position + _offset;
        
        // If player is moving in vertical Axis and it is not the limit position to spawnManager --> Instanciate a obstacle
        if(Input.GetAxis("Vertical") != 0 && _player.transform.position.z < _finish.transform.position.z - _limitSpawn)
            Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform.position, Quaternion.identity);
    }
}
