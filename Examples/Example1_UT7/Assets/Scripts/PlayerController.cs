#if UNITY_IOS || UNITY_ANDROID
    #define USING_MOBILE
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 20;
    private const string ISWALKING = "IsWalking";
    private Vector3 movement;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private Quaternion _rotation = Quaternion.identity;
    private AudioSource _audioSource;   
    
    /// <summary>
    /// Method Start [Life cycle]
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Method FixedUpdate [Life cycle]
    /// </summary>
    void FixedUpdate()
    {
        // Only for Android build
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
        
        // Animator speed change ( OTHER method is -> use an animator parameter)
        _animator.speed = Input.GetKey(KeyCode.Space) ? 2 : 1; 

        // Manage walking audio source
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
    
    /// <summary>
    /// Method OnAnimatorMove
    /// </summary>
    void OnAnimatorMove()
    {
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(_rotation);
    }
}
