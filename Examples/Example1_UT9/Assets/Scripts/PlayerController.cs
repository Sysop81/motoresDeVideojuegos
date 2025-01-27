using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float MAXBREAKFORCE = 3000F;
    
    [Header("Configuración motor")] 
    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;

    [Header("Configuración ruedas lógicas")] 
    [SerializeField] private WheelCollider FL;
    [SerializeField] private WheelCollider FR;
    [SerializeField] private WheelCollider RL;
    [SerializeField] private WheelCollider RR;

    private float currentSteerAngle;
    
    [Header("Configuración del giro")]
    [SerializeField] private float maxSteerAngle;

    [Header("Configuración ruedas físicas")] 
    [SerializeField] private Transform WFL;
    [SerializeField] private Transform WFR;
    [SerializeField] private Transform WRL;
    [SerializeField] private Transform WRR;

    [Header("Velocímetro")] 
    [SerializeField] private float currentSpeed;
    private const float MAXSPEED = 250F;
    private Rigidbody rigidBody;

    [SerializeField] private TextMeshProUGUI currentSpeedText;
    
    private bool _isBreaking;
    private float _giro;
    private float _pedal;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        WriteSpeedText();
    }

    void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    
    private void GetInput()
    {
        _giro = Input.GetAxis("Horizontal");
        _pedal = Input.GetAxis("Vertical");
        
        _isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {

        if (Mathf.Abs(currentSpeed) < MAXSPEED)
        {
            FL.motorTorque = _pedal * motorForce;
            FR.motorTorque = _pedal * motorForce;
        }
        else
        {
            FL.motorTorque = 0;
            FR.motorTorque = 0;
        }

        //FL.motorTorque = _pedal * motorForce;
        //FR.motorTorque = _pedal * motorForce;

        breakForce = _isBreaking ? MAXBREAKFORCE : 0f;
        
        FL.brakeTorque = breakForce;
        FR.brakeTorque = breakForce;
        RL.brakeTorque = breakForce;
        RR.brakeTorque = breakForce;
        
        currentSpeed = rigidBody.velocity.magnitude * 3600 / 1000.0f;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * _giro;
        FL.steerAngle = currentSteerAngle;
        FR.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(FL,WFL);
        UpdateSingleWheel(FR,WFR);
        UpdateSingleWheel(RL,WRL);
        UpdateSingleWheel(RR,WRR);
    }
    
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        
        wheelCollider.GetWorldPose(out position, out rotation);
        
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }

    private void WriteSpeedText()
    {
        
        float speed = currentSpeed > 0 ? currentSpeed : 0;
        currentSpeedText.text = Mathf.Round(speed) + "Km/h";
    }
}
