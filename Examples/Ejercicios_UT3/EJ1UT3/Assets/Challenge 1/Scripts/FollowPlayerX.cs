using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    private GameObject plane;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindGameObjectWithTag("Player");
        offset = new Vector3(20, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = plane.transform.position + offset;
    }
}
