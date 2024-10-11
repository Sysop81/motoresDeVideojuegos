using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTrackLimit : MonoBehaviour
{
    // Track limit
    private const float _LIMIT_TRACK = 0.13f;
    
    
    // Update is called once per frame
    void Update()
    {
        // Call to method
        CheckTrackLimit();
    }
    
    /**
     * Method CheckTrackLimit
     * This method determines the track limit and reset the object X position
     */
    private void CheckTrackLimit()
    {
        if (transform.position.x < -_LIMIT_TRACK)
        {
            transform.position = new Vector3(-_LIMIT_TRACK, transform.position.y, transform.position.z);
        }else if (transform.position.x > _LIMIT_TRACK)
        {
            transform.position = new Vector3(_LIMIT_TRACK, transform.position.y, transform.position.z);
        }
    }
}
