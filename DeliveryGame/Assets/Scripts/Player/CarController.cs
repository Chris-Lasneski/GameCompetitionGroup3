using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class CarController : MonoBehaviour
{
    // largely followed along this tutorial:https://www.youtube.com/watch?v=Z4HA8zJhGEk

    // colliders for the wheels, deals with brake torque and motor torque
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;

    // the wheel objects go into these, needed to make the wheels turn and spin
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    public float acceleration;
    public float maxSteeringAngle;
    private float horizontalInput;
    private float verticalInput;
    private bool brakeing = false;
    private bool ebrake = false;
    private float steeringAngle;
    public float brakeTorque;
    public bool isFrontDrive;
    private float maxSpeed = 36f;

    // modifiers from upgrades, will change when we can draw from upgrade list.
    public float maxSpeedModifier = 0f;
    public float brakeTorqueModifier = 0f;
    // will ned to look into wheel traction, currently doesn't make the car accelrate much faster as much as making the wheels spin faster in a burnout
    public float accelerationModifier = 400f;

    private Vector3 velocity;
    private Vector3 localVel;

    // use to make center of mass to help prevent car from flipping when turning, id recommend only adjusting the y value in the inspector
    // x and z will move it away from middle of car to front back/side to side. lower will do better at keepin it on the "road"
    // cars rigidbody
    public Rigidbody rb;
    // center of mass vector
    public Vector3 com;

    private void Start()
    {
        // get the rigidbody of the car and set the center of mass to the center of mass vector. Helps car not tip over.
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }

    public void FixedUpdate()
    {
        // in functions to not clutter up main loop
        GetInput();
        HandleMotorPower();
        Steering();
        UpdateWheels();
    }

    // seems self explanitory
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        ebrake = Input.GetKey(KeyCode.Space);
    }

    // handles the total power of the motor, do all motor power calcs here. assuming front wheel drive
    private void HandleMotorPower()
    {
        // check if we're at max speed + maxSpeedModifier, if were less add acceleration to the wheels, else keep it at max speed + modifier
        if (rb.velocity.magnitude <= (maxSpeed + maxSpeedModifier))
        {
            // check if front or wheel wheel drive and apply acceleration to proper wheel set.
            if (isFrontDrive)
            {
                frontLeftWheelCollider.motorTorque = verticalInput * (acceleration + accelerationModifier);
                frontRightWheelCollider.motorTorque = verticalInput * (acceleration + accelerationModifier);
            }
            else
            {
                backLeftWheelCollider.motorTorque = verticalInput * (acceleration + accelerationModifier);
                backRightWheelCollider.motorTorque = verticalInput * (acceleration + accelerationModifier);
            }
        }
        else
        {
            if (isFrontDrive)
            {
                frontLeftWheelCollider.motorTorque = verticalInput * (maxSpeed + maxSpeedModifier);
                frontRightWheelCollider.motorTorque = verticalInput * (maxSpeed + maxSpeedModifier);
            }
            else
            {
                backLeftWheelCollider.motorTorque = verticalInput * (maxSpeed + maxSpeedModifier);
                backRightWheelCollider.motorTorque = verticalInput * (maxSpeed + maxSpeedModifier);
            }
        }

        // handles the braking aspect of the car, probably should be moved to new function but fine for now.
        brakeing = isBraking();
        if(brakeing)
        {
            frontRightWheelCollider.brakeTorque = brakeTorque + brakeTorqueModifier;
            frontLeftWheelCollider.brakeTorque = brakeTorque + brakeTorqueModifier;
        }
        else
        {
            frontRightWheelCollider.brakeTorque = 0f;
            frontLeftWheelCollider.brakeTorque = 0f;
        }
        // using this as a full brake to engage brakes even when car is stopped, other brake will start moving in opposite direction when velocity is 0.
        // have it set to back wheels as having it set to front wheel braking along with previous brake set causes 'S' braking to not brake correctly and will intermittenly apply the brakes
        if (ebrake)
        {
            backLeftWheelCollider.brakeTorque = brakeTorque + brakeTorqueModifier;
            backRightWheelCollider.brakeTorque = brakeTorque + brakeTorqueModifier;
        }
        else
        {
            backLeftWheelCollider.brakeTorque = 0f;
            backRightWheelCollider.brakeTorque = 0f;
        }
    }

    // checks to see if car is breaking by pressing reverse, then switch to reverse and check for forward input. doens't care about ebrake
    private bool isBraking()
    {
        velocity = rb.velocity;
        localVel = transform.InverseTransformDirection(velocity);

        if(localVel.z > 0f && Input.GetKey(KeyCode.S))
        {
            return true;
        }
        else if(localVel.z < 0f && Input.GetKey(KeyCode.W))
        {
            return true;
        }
        return false;
    }

    // deals with the steering of the car, currently only supports front wheel steering cars. don't know if rear wheel steering would make a diffenrce at the moment
    private void Steering()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steeringAngle;
        frontRightWheelCollider.steerAngle = steeringAngle;
    }

    // helper function dealing with wheel updates, reduces clutter in main loop
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    // updates a single wheel making them spin and move with car
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}