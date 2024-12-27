using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(-10,10), SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float turnSpeed = 45f;
    private float _horizontalInput, _verticalInput;

    private const float ROAD_LIMIT = 8f;
    private float _initialZposition;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get initial Z position to set start road limit
        _initialZposition = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        // Get Axis
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        
        // Old translate example
        //this.transform.Translate(speed * Time.deltaTime * Vector3.forward);
        
        
        // Player forward move
        this.transform.Translate(speed * _verticalInput * Time.deltaTime * Vector3.forward);
        
        // Set road limits (x and z positions)
        if(transform.position.x <= -ROAD_LIMIT)
            transform.position = new Vector3(-ROAD_LIMIT, transform.position.y, transform.position.z);
        if (transform.position.x >= ROAD_LIMIT) 
            transform.position = new Vector3(ROAD_LIMIT, transform.position.y, transform.position.z);
        if(transform.position.z <= _initialZposition)
            transform.position = new Vector3(transform.position.x, transform.position.y, _initialZposition);
        
        // Player horizontal move only if move up or down.
        if (Mathf.Abs(_verticalInput) > 0) 
            transform.Rotate(turnSpeed * _horizontalInput * Time.deltaTime * Vector3.up);
    }
}
