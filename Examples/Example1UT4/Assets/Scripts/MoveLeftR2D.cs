using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftR2D : MonoBehaviour
{

    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left * 18f, ForceMode.Impulse);
    }
    
}
