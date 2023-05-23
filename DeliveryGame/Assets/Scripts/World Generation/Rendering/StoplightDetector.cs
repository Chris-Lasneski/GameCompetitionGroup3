using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StoplightDetector : MonoBehaviour {
    private bool hasEnteredCollider = false;
    


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Rigidbody target = LawEnforcementController.playerInfo.currentCar.GetComponent<Rigidbody>();
        Vector3 playerDirection = target.velocity;
        float dotProduct = Vector3.Dot(playerDirection.normalized, transform.forward);

        if (dotProduct > 0 && hasEnteredCollider) {
            LawEnforcementController.reportRed();
            hasEnteredCollider = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Transform target = LawEnforcementController.playerInfo.currentCar.transform;
        if (other.transform == target && !hasEnteredCollider) {
            hasEnteredCollider = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Transform target = LawEnforcementController.playerInfo.currentCar.transform;
        if (other.transform == target && hasEnteredCollider) {
            hasEnteredCollider = false;
        }
    }
}
