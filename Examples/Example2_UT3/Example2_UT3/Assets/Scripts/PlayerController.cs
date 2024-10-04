using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float horizontalInput;

    [SerializeField] private float speed = 10f;

    private const float _LIMIT = 17.1f;
    
    [SerializeField] private GameObject projectile;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Player move
        this.transform.Translate( horizontalInput * speed * Time.deltaTime * Vector3.right);
        
        // Set left and rigth limit by tranform position 
        if(transform.position.x < -_LIMIT)
            this.transform.position = new Vector3(-_LIMIT, transform.position.y, transform.position.z);
        
        if(transform.position.x > _LIMIT)
            this.transform.position = new Vector3(_LIMIT, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("proyectile pos -> " + transform.position);
            Instantiate(projectile, this.transform.position, this.transform.rotation);
        }
            
    }
}
