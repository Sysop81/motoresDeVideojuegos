using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        // Destroy me and other gameObject
        Destroy(gameObject);
        Destroy(other.gameObject);
    }

}
