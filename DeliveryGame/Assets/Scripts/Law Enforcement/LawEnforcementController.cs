using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawEnforcementController
{
    public static PlayerInfo playerInfo;

    public static void reportSpeeding(float speed) {
        playerInfo.Money -= LawEnforcementConstants.CameraSpeedCost;
    }

    public static void reportRed() {
        playerInfo.Money -= LawEnforcementConstants.CameraRedCost;
    }
}
