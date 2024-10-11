using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingManager : MonoBehaviour
{
    // Tilt angle
    private float angle = 15f;
    
    /**
     * Method countBowling
     * This method counts the bowlings that are standing
     */
    public int CountBowling()
    {
        
        // Init bolwling counter to zero
        int count = 0;
        
        // Go through the bowling of the empty
        foreach (Transform bowling in transform)
        {
            // Get angles
            var rotation = bowling.eulerAngles;
            
            // Get position and rotations of XYZ axes
            float tiltX = Mathf.Abs(rotation.x);
            float posY = bowling.transform.position.y;
            float tiltZ = Mathf.Abs(rotation.z);

            // Check if bowling in on track and check angles
            if (posY > 0 && (tiltX < angle || tiltX > 360 - angle))
            {
                if (tiltZ < angle || tiltZ > 360 - angle)
                {
                    count++;
                }
            }
        }
        return count;
    }
}
