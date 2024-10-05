using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerY : MonoBehaviour
{
    
    [SerializeField] private GameObject[] balls;
    
    private float _initialRangeX = -36f;
    private float _endRangeX = 9f;
    private int _index;
    
    // Start is called before the first frame update
    void Start()
    {
        // Automatic invoke
        InvokeRepeating(nameof(CreateBall),0,2);
    }

    /**
     * Method CreateBall
     */
    private void CreateBall()
    {
        // Generate a randon X axis position 
        var posX = Random.Range(_initialRangeX, _endRangeX);
        
        // Generate a new vector 3 with the ball position
        Vector3 position = new Vector3(posX, 28.2f, 2.6f);
        
        // Get a random index
        _index = Random.Range(0, balls.Length);
        
        // Instanciate a new ball
        Instantiate(balls[_index], position, Quaternion.identity);
    }
}
