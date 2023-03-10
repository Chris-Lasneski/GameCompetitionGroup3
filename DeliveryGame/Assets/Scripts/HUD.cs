using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // text output to speedometer
    public Text speed;
    // get the car's rigid body to calculate the velocity
    public Rigidbody car;

    // used to deal with the math to clamp to an int and extract from rigidbody.
    private float velocity;

    // Start is called before the first frame update
    void Start()
    {
        // inital value
        speed.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // get the velocity of the car and multiply by 2.237 to get mph, might need to change value depending on the scale of world
        velocity = car.velocity.magnitude * 2.237f;
        speed.text = (Mathf.Round(velocity).ToString());
        speed.text += "MPH";
    }
}
