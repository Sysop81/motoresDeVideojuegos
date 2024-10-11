using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Range(1,5), SerializeField]
    private float speed = 2f;
    [SerializeField] private GameObject ballPrefab;
    
    
    // Update is called once per frame
    void Update()
    {
        
        // Player horizontal move
        transform.Translate(speed * Input.GetAxis("Horizontal") * Time.deltaTime * Vector3.left);

        // If press Space && game is not paused -> Create a ball object
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {
            Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
    }
}
