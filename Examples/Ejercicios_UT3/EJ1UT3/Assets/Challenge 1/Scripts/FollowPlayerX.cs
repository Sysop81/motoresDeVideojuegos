using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private Vector3 offset = new Vector3(40, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        // Instanciate player gameObject 
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Update the camera position
        transform.position = _player.transform.position + offset;
    }
}
