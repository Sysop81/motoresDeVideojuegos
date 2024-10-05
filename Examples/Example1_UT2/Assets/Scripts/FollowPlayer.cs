using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(0, 5, -8);
    }

    // Update is called once per frame
    void Update()
    {
        // Set a main camera position with player position + offset
        transform.position = player.transform.position + offset;
    }
}
