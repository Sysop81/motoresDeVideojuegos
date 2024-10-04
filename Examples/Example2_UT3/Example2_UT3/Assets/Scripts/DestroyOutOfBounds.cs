using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{

    [SerializeField] private float upBound = 30f;
    [SerializeField] private float downBound = -18f;   
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.z > upBound  || transform.position.z < downBound)
        {
            Destroy(gameObject);
        }
    }
}
