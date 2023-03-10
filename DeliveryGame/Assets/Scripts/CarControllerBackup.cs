using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerBackup : MonoBehaviour
{

    // IMPORTANT for first build
    // For now the code is heavily based off Cyber Chroma's tutorial for creating a car controller in Unity: https://www.youtube.com/watch?v=Ul01SxwPIvk
    // I plan to come back and heavily modify it later when we're further along in development and more pieces, such as an acutal car with wheels is implemented to add colliders and implement more variables into the speed/acceleration calculations

    private float speed = 25.0f;
    private float turnspeed = 25.0f;
    private float velocity = 0.0f;
    private float acceleration = 1.0f;
    private float mass = 1.5f;
    private float topSpeed = 25.0f;
    private float reverseTopSpeed = 5.0f;
    private float breakTorque = 1.0f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move() {
        // forward and backward
        if(Input.GetKey(KeyCode.W)) {
            if (velocity < topSpeed)
            {
                velocity += acceleration;
            }
            else if(velocity == topSpeed)
            {
                velocity += 0.0f;
            }
            else
            {
                velocity -= mass;
            }
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * velocity);
        }
        else if(Input.GetKey(KeyCode.S)) {
            if (velocity >= reverseTopSpeed)
            {
                velocity -= breakTorque;
            }
            rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -speed);        
        }
        Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
        localVelocity.x = 0;
        rb.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn() {
        //Turning
        if (velocity > 0.0f)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddTorque(Vector3.up * turnspeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rb.AddTorque(-Vector3.up * turnspeed);
            }
        }
    }
}
