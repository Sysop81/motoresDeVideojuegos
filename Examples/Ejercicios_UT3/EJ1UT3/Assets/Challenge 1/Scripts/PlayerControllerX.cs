using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10f;
    [SerializeField]
    private float rotationSpeed = 45f;
    private float _verticalInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        _verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(speed  * Time.deltaTime * Vector3.forward);

        // tilt the plane up/down based on up/down arrow keys
        this.transform.Rotate( rotationSpeed * _verticalInput * Time.deltaTime * Vector3.right);
        
        // Recalculate speed on base a _verticalInput 
        speed = Mathf.Abs(_verticalInput) == 0 ? 10f : 8f;
    }
}
