using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text time;
    public PlayerInfo player;
    public GameObject ui;

    public bool started;

    //public MissionController missionController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //started = MissionController.isStarted;
        //if (started)
        //{
        //    ui.SetActive(true);
        //}
        //else if (!started)
        //{
        //    ui.SetActive(false);
        //}
        time.text = "Time: " + MissionController.timer.ToString() + "\t\tCurrent Location: " + player.currentCar.transform.position + "\t\tGoal: " + MissionController.mission.endLocation;
    }
}
