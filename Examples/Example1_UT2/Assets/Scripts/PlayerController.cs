using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(-10,10), SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float turnSpeed = 45f;
    private float horizontalInput, verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Get Axis
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        
        // Old translate example
        //this.transform.Translate(speed * Time.deltaTime * Vector3.forward);
        
        
        // Player forward move
        this.transform.Translate(speed * verticalInput * Time.deltaTime * Vector3.forward);
        
        // Player horizontal move
        if (Mathf.Abs(verticalInput) > 0)// Only if player is moving 
            this.transform.Rotate(turnSpeed * horizontalInput * Time.deltaTime * Vector3.up);
    }
}
