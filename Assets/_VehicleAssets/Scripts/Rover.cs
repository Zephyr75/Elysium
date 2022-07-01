using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rover : Vehicle
{
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;


    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider middleLeftWheelCollider;
    [SerializeField] private WheelCollider middleRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform middleLeftWheelTransform;
    [SerializeField] private Transform middleRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    
    private void FixedUpdate()
    {
        base.FixedUpdate();

        //isBreaking = jump;
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        GetComponent<Rigidbody>().centerOfMass = new Vector3 (0, -2, 0);
    }


    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        if (Mathf.Abs(verticalInput) < .1f)
        {
            //currentbreakForce = isBreaking ? breakForce : 1000f;
            //ApplyBreaking(10000f);   
        }
        else
        {
            //ApplyBreaking(0); 
        }
    }

    private void ApplyBreaking(float force)
    {
        frontRightWheelCollider.brakeTorque = force;
        frontLeftWheelCollider.brakeTorque = force;
        middleRightWheelCollider.brakeTorque = force;
        middleLeftWheelCollider.brakeTorque = force;
        rearLeftWheelCollider.brakeTorque = force;
        rearRightWheelCollider.brakeTorque = force;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(middleLeftWheelCollider, middleLeftWheelTransform);
        UpdateSingleWheel(middleRightWheelCollider, middleRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        //wheelTransform.rotation = wheelCollider.transform.rotation;
        //wheelTransform.position = wheelCollider.transform.position;
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
