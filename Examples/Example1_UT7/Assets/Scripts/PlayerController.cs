#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private string ISWALKING = "IsWalking";
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    [SerializeField] private float turnSpeed = 20;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        #if USING_MOBILE
            float horizontal = Input.GetAxis("MouseX");
            float vertical = Input.GetAxis("MouseY");
            if (Input.touchCount > 0)
            {
                horizontal = Input.touches[0].deltaPosition.x;
                vertical = Input.touches[0].deltaPosition.y;
            }
        #else
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
        #endif

        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        _animator.SetBool(ISWALKING, isWalking);

        Vector3 desiredForward =
            Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.fixedDeltaTime, 0f);
        _rotation = Quaternion.LookRotation(desiredForward);

        _animator.speed = Input.GetKey(KeyCode.Space) ? 2 : 1; // Change to use a animator parameter

        if (isWalking)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
        else
        {
            _audioSource.Stop();
        }        
    }
    
    void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
    }
}
