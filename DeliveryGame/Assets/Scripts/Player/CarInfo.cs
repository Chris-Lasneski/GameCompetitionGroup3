using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour

{
    // what is the cars base top speed
    public float topSpeed = 0.0f;
    // cars base accerlation rate
    public float acceleration = 0.0f;
    // base braking force
    public float brakTorque = 0.0f;
    public float weight = 0.0f;

    
    //modifier variables used when the player has a modification that increases how fast they accelerate, topspeed, etc.
    public float topSpeedModifier = 0.0f;
    public float accelerationModifier = 0.0f;
    public float brakTorqueModifier = 0.0f;


    // don't think we'll need these functions. Plan on just having this to store variables for each type of car to be used in other scripts

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
