using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    private GameObject _player;
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = new Vector3(0, 0, 20);
        _player = GameObject.FindGameObjectWithTag("Player");
        
        InvokeRepeating("SpawnRandomObstacle",0,2f);
    }
    
    // Method SpawnRandomObstacle
    void SpawnRandomObstacle()
    {
        // Player position + offset to set obstacle position
        this.transform.position = _player.transform.position + _offset;
        
        // Select random obstacle from array
        int obstacleIndex = Random.Range(0, obstacles.Length);
        
        // If player is moving forward --> Instanciate a obstacle
        if(Input.GetAxis("Vertical") > 0)
            Instantiate(obstacles[obstacleIndex], this.transform.position, Quaternion.identity);
    }
}
