using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class NPCCarController : MonoBehaviour {
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
    private float steeringAngle;
    public float brakeTorque;
    public bool isFrontDrive;

    // modifiers from upgrades, will change when we can draw from upgrade list.
    public float maxSpeedModifier = 0f;
    public float brakeTorqueModifier = 0f;
    // will ned to look into wheel traction, currently doesn't make the car accelrate much faster as much as making the wheels spin faster in a burnout
    public float accelerationModifier = 400f;

    // use to make center of mass to help prevent car from flipping when turning, id recommend only adjusting the y value in the inspector
    // x and z will move it away from middle of car to front back/side to side. lower will do better at keepin it on the "road"
    // cars rigidbody
    public Rigidbody rb;
    // center of mass vector
    public Vector3 com;

    private void Start() {
        // get the rigidbody of the car and set the center of mass to the center of mass vector. Helps car not tip over.
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
    }

    public void FixedUpdate() {
        // in functions to not clutter up main loop
        Steering();
        UpdateWheels();
    }

    // deals with the steering of the car, currently only supports front wheel steering cars. don't know if rear wheel steering would make a diffenrce at the moment
    private void Steering() {
        frontLeftWheelCollider.steerAngle = steeringAngle;
        frontRightWheelCollider.steerAngle = steeringAngle;
    }

    // helper function dealing with wheel updates, reduces clutter in main loop
    private void UpdateWheels() {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    // updates a single wheel making them spin and move with car
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}