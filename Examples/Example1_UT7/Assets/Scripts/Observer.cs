using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] private GameEnding gameEnding;
    private bool _isPlayerInRange = false;
    

    /// <summary>
    /// Method Update [Life cycle]
    /// Update is called once per frame
    /// </summary>
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
                    //Debug.Log("Player is in enemy range ");
                    gameEnding.CaughtPlayer();
                    _isPlayerInRange = false;
                }
            }
        }
    }
    

    /// <summary>
    /// Trigger OnTriggerEnter
    /// </summary>
    /// <param name="other">Collision gameObject</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == playerTransform)
        {
            _isPlayerInRange = true;
        }
    }
    
    /// <summary>
    /// Trigger OnTriggerExit
    /// </summary>
    /// <param name="other">Collision gameObject</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == playerTransform)
        {
            _isPlayerInRange = false;
        }
    }
    
    /// <summary>
    /// Method OnDrawGizmos
    /// This method draw Observer Gizmos, sphere for focalPoint and line to player
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, playerTransform.position + Vector3.up /2);
    }
}
