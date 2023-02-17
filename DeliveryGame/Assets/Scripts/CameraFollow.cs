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

    public float mouseSens = 4.0f;

    private Vector3 offset;

    //For this Version used Cyber Chroma's camera follow script:https://www.youtube.com/watch?v=Ul01SxwPIvk
    // No idea if we'll keep this follow script or come back and change it later on.

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(player.position.x, player.position.y, player.position.z+5.0f);
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
            offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * mouseSens, Vector3.up) * offset;

            transform.position = player.position + offset;
            transform.LookAt(player.position);
            //if(Input.GetAxis("Mouse X") > 0)
            //{
            //    transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens);
            //}
            //else
            //{
            //    transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSens, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSens);
            //}
        }
    }
}
