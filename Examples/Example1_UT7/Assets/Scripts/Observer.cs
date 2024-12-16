using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    private bool _isPlayerInRange = false;
    [SerializeField] private GameEnding gameEnding;
    
    

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerInRange)
        {
            Vector3 direction = playerTransform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(transform.position, direction, Color.red,Time.deltaTime,true);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == playerTransform)
                {
                    Debug.Log("Player is in Gargoyle range ");
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == playerTransform)
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == playerTransform)
        {
            _isPlayerInRange = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, playerTransform.position + Vector3.up /2);
    }
}
