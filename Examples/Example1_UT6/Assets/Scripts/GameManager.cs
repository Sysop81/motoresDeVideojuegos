using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    [SerializeField] private float spawnRate = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            var index = Random.Range(0, targets.Count);
            Instantiate(targets[index],transform.position,targets[index].transform.rotation);
        }
    }
}
