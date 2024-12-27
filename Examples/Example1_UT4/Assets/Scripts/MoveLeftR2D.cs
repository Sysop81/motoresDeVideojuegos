using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftR2D : MonoBehaviour
{

    private Rigidbody rb;
    private GameObject son;
    
    // Start is called before the first frame update
    void Start()
    {
        
        son = transform.GetChild(0).gameObject;
        
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.left * 18f, ForceMode.Impulse);
        
    }

    void Update()
    {
        son.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}
