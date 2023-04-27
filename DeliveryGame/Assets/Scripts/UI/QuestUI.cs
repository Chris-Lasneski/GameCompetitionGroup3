using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    //main body of text in ui
    public Text questDescription;
    public GameObject ui;


    private Vector2 start;
    private Vector2 end;
    private float estimate;
    private float potentialReward;
    public bool started;

    public void MissionInfo()
    {
        //start = missionDetails.startLocation;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        end = MissionController.mission.endLocation;
        estimate = MissionController.mission.estimatedTime;
        potentialReward = MissionController.mission.reward;
        //started = MissionController.isStarted;

        //if (started)
        //{
        //    ui.SetActive(true);
        //}
        //else if (!started)
        //{
        //    ui.SetActive(false);
        //}

        questDescription.text = "Mission Goal: " + end + System.Environment.NewLine;
        questDescription.text += "Estimated Delivery Time: " + estimate + System.Environment.NewLine;
        questDescription.text += "Potential Payout: $" + potentialReward + System.Environment.NewLine;
    }
}
