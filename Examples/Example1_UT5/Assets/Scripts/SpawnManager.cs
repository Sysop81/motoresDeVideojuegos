using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public int enemyWave = 1;
    public int enemyCount;
    private float spawnRange = 9f;
    private float spawnPosX, spawnPosZ;
    
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(enemy, GenerateSpawnPosition(), enemy.transform.rotation);
        
        SpawnEnemyWave(enemyWave);
    }

    private void Update()
    {
        // TODO REPAIR THIS
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            SpawnEnemyWave(enemyWave);
        }
    }

    /// <summary>
    /// Generate a enemy aleatory position of spawnRange
    /// </summary>
    /// <returns></returns>
    private Vector3 GenerateSpawnPosition()
    {
        spawnPosX = Random.Range(-spawnRange, spawnRange);
        spawnPosZ = Random.Range(-spawnRange, spawnRange);
        
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    private void SpawnEnemyWave(int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; i++)
        {
            Instantiate(enemy, GenerateSpawnPosition(), enemy.transform.rotation);
        }
    }
}
