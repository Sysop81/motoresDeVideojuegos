using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private float moveForce = 3f;
    [SerializeField] private float forwardInput;
    [SerializeField] private float restartGamePlayTime = 3f;
    [SerializeField] private float powerUpForce = 20f;
    [SerializeField] private GameObject[] powerUpRings;
    
    private GameObject _focalPoint;
    private bool _hasPowerUp = false;
    private float _powerUpTime = 7f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(forwardInput * moveForce * _focalPoint.transform.forward, ForceMode.Force);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            //Destroy(gameObject);
            Invoke("RestartGame",restartGamePlayTime);
        }

        if (other.gameObject.CompareTag("PowerUp"))
        {
            _hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDown());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && _hasPowerUp)
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;
            otherRB.AddForce(awayFromPlayer * powerUpForce, ForceMode.Impulse);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator PowerUpCountDown()
    {
        for (int i = 0; i < powerUpRings.Length; i++)
        {
            powerUpRings[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(_powerUpTime / powerUpRings.Length);
            powerUpRings[i].SetActive(false);
        }
        _hasPowerUp = false;
    }
}
