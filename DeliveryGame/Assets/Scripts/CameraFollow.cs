using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing;
    public float rotSmoothing;
    public Transform player;

    public float mouseSens = 1.0f;
    public float clampAngle = 80.0f;

    //For this Version used Cyber Chroma's camera follow script:https://www.youtube.com/watch?v=Ul01SxwPIvk
    // No idea if we'll keep this follow script or come back and change it later on.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
            transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotSmoothing);
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
        else
        {
            

        }
    }
}
