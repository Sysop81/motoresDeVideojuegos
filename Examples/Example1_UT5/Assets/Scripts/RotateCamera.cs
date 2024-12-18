using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       horizontalInput = Input.GetAxis("Horizontal");
       transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }
}
