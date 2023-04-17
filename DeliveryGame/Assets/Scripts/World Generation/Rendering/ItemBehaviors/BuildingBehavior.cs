using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehavior : MonoBehaviour
{
    public Vector2 position;
}

public class MissionBuildingBehavior : BuildingBehavior {
    private void OnTriggerEnter(Collider other) {
        MissionController.checkMissionTrigger(position);
    }
}