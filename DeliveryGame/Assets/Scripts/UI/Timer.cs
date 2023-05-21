using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + MissionController.timer.ToString() + "\t\tGoal: " + MissionController.mission.endLocation;
    }
}
