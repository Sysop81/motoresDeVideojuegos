using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    public GameObject smokeEffect;
    private ParticleSystem _smokeEffectParticleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        _smokeEffectParticleSystem = smokeEffect.GetComponent<ParticleSystem>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(verticalInput * speed * Time.deltaTime * focalPoint.transform.forward);

        // Manage player particle system
        ManageSmokeParticleSystem();
        
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, 0, 0);
    }
    
    /// <summary>
    /// Method ManageSmokeParticleSystem
    /// This method manage the smoke particle system. This is activated when plyer velocity i s greater than a
    /// normalStrength variable
    /// </summary>
    private void ManageSmokeParticleSystem()
    {
        // If player velocity is greater than normal velocity && skome effect is not playing, we launch the effect
        if (playerRb.velocity.magnitude > normalStrength && !_smokeEffectParticleSystem.isPlaying)
            _smokeEffectParticleSystem.Play();
        
        // Adding smoke particle system to player
        if(_smokeEffectParticleSystem.isPlaying) 
            smokeEffect.transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }
    

    /// <summary>
    /// Trigger OnTriggerEnter
    /// If Player collides with powerup, activate powerup
    /// </summary>
    /// <param name="other">GameObject that launch the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    /// <summary>
    /// Corrutine PowerupCooldown
    /// This method count down powerup duration
    /// </summary>
    /// <returns></returns>
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    
    /// <summary>
    /// Method OnCollisionEnter [Callback]
    /// If Player collides with enemy
    /// </summary>
    /// <param name="other">GameObject that launch the callback</param>
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && playerRb.velocity.magnitude > 1)
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (other.gameObject.transform.position - transform.position).normalized; 
            
            enemyRigidbody.AddForce(awayFromPlayer * (hasPowerup ? powerupStrength : normalStrength), ForceMode.Impulse);
        }
    }
}
