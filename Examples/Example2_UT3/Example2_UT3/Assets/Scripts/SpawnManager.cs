using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Properties
    [SerializeField] private GameObject[] animals;
    [SerializeField] private int index;

    private float _spawnRangeX = 22.1f;
    private float _spawnRangeZ;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnRangeZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // If press kay S -> instanciate a new animal gameObject
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Generate a randon X axis position 
            var posX = Random.Range(-_spawnRangeX, _spawnRangeX);
            
            // Generate a new vector 3 with the animal position
            Vector3 position = new Vector3(posX, 0, _spawnRangeZ);
            
            // Get a randon index from animal, to get animal type.
            index = Random.Range(0, animals.Length);
            
            // Instanciate a new animal
            Instantiate(animals[index], position, animals[index].transform.rotation);
        }
    }
}
