using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCameraBehavior : MonoBehaviour
{
    bool hasTicketed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        GameObject go = other.gameObject;
        CarController controller = go.GetComponent<CarController>();
        if(controller != null) {
            float speed = 2.237f * controller.rb.velocity.magnitude;
            if (speed > LawEnforcementConstants.CameraSpeedLimit && !hasTicketed) {
                LawEnforcementController.reportTicket(speed);
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
