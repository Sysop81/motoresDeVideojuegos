using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;
    private GameObject _spawnManager; 

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        playerGoal = GameObject.Find("Player Goal");
        _spawnManager = GameObject.Find("Spawn Manager");
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(speed * Time.deltaTime * lookDirection);
    }
    
    /// <summary>
    /// Method OnCollisionEnter [Callback]
    /// </summary>
    /// <param name="other">GameObject that launch the callback</param>
    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal" || other.gameObject.name == "Player Goal")
        {
            // In any case the object is destroyed
            Destroy(gameObject);
            
            // Call the spawn manager to launch new enemy wave
            if(GameObject.FindObjectsOfType<EnemyX>().Length - 1 == 0)
                _spawnManager.GetComponent<SpawnManagerX>().LaunchNewEnemyWave();
        } 
    }

}
