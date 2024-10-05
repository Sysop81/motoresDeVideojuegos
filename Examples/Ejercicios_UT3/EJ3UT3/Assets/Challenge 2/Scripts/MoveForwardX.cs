using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardX : MonoBehaviour
{
    public float speed;

    private const float LIMIT = -40f;
    // Update is called once per frame
    void Update()
    {
        // If Dog is under limit then destroy the dog gameObject and set return to exit
        if (transform.position.x <= LIMIT)
        {
            Destroy(gameObject);
            return;
        }
        
        // Else translate the dog for X axis
        transform.Translate( speed * Time.deltaTime * Vector3.forward);
    }
}
