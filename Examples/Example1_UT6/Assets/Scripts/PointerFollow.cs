using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollow : MonoBehaviour
{
    [SerializeField] private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector2(mousePos.x, mousePos.y);
        transform.position = mousePos;
    }
}
