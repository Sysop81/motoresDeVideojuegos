using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        
        /*if (other.CompareTag("Ground"))
        {
            //Debug.Log("¡¡¡¡ GAME OVER !!!!");
            //Time.timeScale = 0;
            //Destroy(gameObject);
            
        }
        else
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }*/

        // If collision with Dog -> destroy dog
        if (other.CompareTag("Dog")) Destroy(other.gameObject);
        
        // Always destroy the ball (on collision with dog or ground)
        Destroy(gameObject);
    }
}
