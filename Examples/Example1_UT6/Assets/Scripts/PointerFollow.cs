using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    
    /// <summary>
    /// Method Update [Lifes cycles]
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        var mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePos.x, mousePos.y);
    }
}
