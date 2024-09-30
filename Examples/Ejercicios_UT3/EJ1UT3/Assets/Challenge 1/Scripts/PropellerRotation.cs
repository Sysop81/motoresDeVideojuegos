using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 500f;
    
    // Update is called once per frame
    void Update()
    {
        // Apply the constant rotation to plane propeller
        this.transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.back);
    }
}
