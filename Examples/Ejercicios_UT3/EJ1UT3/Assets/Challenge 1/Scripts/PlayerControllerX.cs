using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 45f;
    public float verticalInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        //horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(/*Vector3.back * speed*/ speed  * Time.deltaTime * Vector3.forward);

        // tilt the plane up/down based on up/down arrow keys
        this.transform.Rotate( rotationSpeed * verticalInput * Time.deltaTime * Vector3.right);
        //transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
