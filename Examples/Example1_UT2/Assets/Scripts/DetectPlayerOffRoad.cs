using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerOffRoad : MonoBehaviour
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
        
        // is Player ???
        if (other.CompareTag("Player"))
        {
            Debug.Log("End of road. Reset player position");
            player.transform.position = new Vector3(4, 0, -5);
        }
    }
}
