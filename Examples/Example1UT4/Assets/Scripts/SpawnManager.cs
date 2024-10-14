using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    private float startDelay = 1.0f;
    private float spawnInterval = 2.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomObstacle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void SpawnRandomObstacle()
    {
        var index = Random.Range(0, obstaclesPrefabs.Length);
        // Generate random obstacle index and random spawn position
        //Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

        // instantiate ball at random spawn location
        Instantiate(obstaclesPrefabs[index], transform.position, obstaclesPrefabs[index].transform.rotation);
    }
}
