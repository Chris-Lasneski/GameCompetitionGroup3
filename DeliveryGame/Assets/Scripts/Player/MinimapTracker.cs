using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTracker : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Vector3 offset;
    public bool rotate = false;

    void LateUpdate() {
        transform.position = playerInfo.currentCar.transform.position + offset;
        transform.rotation = rotate ? Quaternion.Euler(0, playerInfo.currentCar.transform.rotation.eulerAngles.y, 0) : Quaternion.Euler(0,0,0);
    }
}
