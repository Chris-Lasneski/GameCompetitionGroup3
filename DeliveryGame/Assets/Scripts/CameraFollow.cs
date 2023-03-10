using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float sens = 0.5f;
    public Vector2 turn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        turn.y += Input.GetAxis("Mouse Y") * sens;
        turn.x += Input.GetAxis("Mouse X") * sens;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        //if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        //{
        //    transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
        //    transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotSmoothing);
        //    transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        //}
    }
}
