using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDetails {
    private static float timeScalar = .05f;
    private static float rewardScalar = 20;

    public Vector2 startLocation;
    public Vector2 endLocation;

    public float estimatedTime;
    public float reward;

    public MissionDetails(Vector2 startLocation, Vector2 endLocation) {
        this.startLocation = startLocation;
        this.endLocation = endLocation;

        estimatedTime = (Mathf.Abs(startLocation.x - endLocation.x) + Mathf.Abs(startLocation.y - endLocation.y)) * timeScalar;
        reward = estimatedTime * rewardScalar;
    }
}
