using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTracker : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool rotate = false;

    void LateUpdate() {
        transform.position = player.position + offset;
        transform.rotation = rotate ? Quaternion.Euler(0, player.rotation.eulerAngles.y, 0) : Quaternion.Euler(0,0,0);
    }
}
