using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private bool isOnGround = true;
    private bool gameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
        }else if (other.gameObject.CompareTag("Obstacles"))
        {
            gameOver = true;
            Debug.Log("Game Over");
        }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
}
