using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    private float startDelay = 1.0f;
    private float spawnInterval = 2.0f;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnRandomObstacle", startDelay, spawnInterval);
    }

    /*private void Update()
    {
        if(!player.GameOver) CancelInvoke("SpawnRandomObstacle");
    }*/

    // Update is called once per frame
    void SpawnRandomObstacle()
    {
        // Set Game Over calling player gameOver method
        if(player.GameOver) CancelInvoke("SpawnRandomObstacle");
        
        var index = Random.Range(0, obstaclesPrefabs.Length);
        // Generate random obstacle index and random spawn position
        //Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(obstaclesPrefabs[index], transform.position, obstaclesPrefabs[index].transform.rotation);
    }
}
