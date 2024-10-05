using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // This trigger check if player is end of road
    private void OnTriggerEnter(Collider other)
    {
        // is Obstacle ???
        /*if (CompareTag("Obstacle"))
        {
            
            Debug.Log("GAME OVER !!! CRASH WITH -> " + other.name);
            Time.timeScale = 0;
            // Restart player position
            //transform.position = new Vector3(4, 0, -5);
        }
        else if (CompareTag("Finish"))
        {
            Debug.Log("FINISH -> YOU WIN!!!!");
            Time.timeScale = 0;
        }*/

        // Build the MSG
        var msg = CompareTag("Obstacle") ? "GAME OVER !!! CRASH WITH -> " + name :
            CompareTag("Finish") ? "FINISH -> YOU WIN!!!!" : "";
        
        // If msg is not empty print the msg and stop gameplay
        if (msg != "")
        {
            Debug.Log(msg);
            Time.timeScale = 0;
        }

        
    }
}
