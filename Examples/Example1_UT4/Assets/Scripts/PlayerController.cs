using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private bool isOnGround = true;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private ParticleSystem dirty;
    [SerializeField] private AudioClip jumpSound,deathSound;
    [SerializeField] private AudioSource audioSource;
    private bool gameOver = false;
    private Animator animator;
    private Rigidbody playerRb;

    private const string JUMP_TRIG = "Jump_trig";
    private const string DEATH_TYPE = "DeathType_int";
    private const string DEATH_B = "Death_b";
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); 
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround){
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            
            animator.SetTrigger(JUMP_TRIG);
            isOnGround = false;
            
            audioSource.PlayOneShot(jumpSound,1);
            
            dirty.Stop();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground")){
            isOnGround = true;
            dirty.Play();
        }else if (other.gameObject.CompareTag("Obstacles"))
        {
            gameOver = true;
            Debug.Log("Game Over");
            
            explosion.Play();
            animator.SetInteger(DEATH_TYPE,Random.Range(1,3));
            animator.SetBool(DEATH_B,true);
            
            audioSource.PlayOneShot(deathSound);
            
            Invoke("RestartGame",3.0f);
        }
    }

    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
