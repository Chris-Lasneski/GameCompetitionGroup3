using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCameraBehavior : MonoBehaviour
{
    bool hasTicketed = false;

    private void OnTriggerStay(Collider other) {
        GameObject go = other.gameObject;
        CarController controller = go.GetComponent<CarController>();
        if(controller != null) {
            float speed = 2.237f * controller.rb.velocity.magnitude;
            if (speed > LawEnforcementConstants.CameraSpeedLimit && !hasTicketed) {
                LawEnforcementController.reportSpeeding(speed);
                hasTicketed = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        GameObject go = other.gameObject;
        CarController controller = go.GetComponent<CarController>();
        if (controller != null) {
            hasTicketed=false;
        }
    }
}
