using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MissionController {
    
    public static WorldRenderer world;
    public static PlayerInfo playerInfo;

    public static MissionDetails mission = null;
    public static bool isStarted = false;
    public static GameObject beacon;

    public static float timer = 0;

    public static GameObject timerUI;
    public static GameObject questUI;
    private static bool q = false;

    public static void fixedUpdate() {
        timer += Time.deltaTime;
        if (isStarted)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                q = !q;
                questUI.SetActive(q);
            }
        }
    }

    public static void initMissions(WorldRenderer world, GameObject beaconPfb, PlayerInfo playerInfo, GameObject time, GameObject quest) {
        MissionController.world = world;
        MissionController.playerInfo = playerInfo;
        timerUI = time;
        questUI = quest;

        beacon = MonoBehaviour.Instantiate(beaconPfb, new Vector3(0,0,0), new Quaternion());
        createRandomMission();
    }

    private static void createRandomMission() {
        Vector2 start = getMissionBuilding(world.chunkPos);
        int ind = Random.Range(0, 8);
        Vector2Int endChunk = world.chunkPos + Map.externalIndexToDirection[0];
        Vector2 end = getMissionBuilding(endChunk);
        mission = new MissionDetails(start, end);
        beacon.transform.SetPositionAndRotation(new Vector3(mission.startLocation.x, 0, mission.startLocation.y), new Quaternion());
    }

    public static Vector2 getMissionBuilding(Vector2Int chunkPos) {
        ChunkData chunk = world.chunkList[chunkPos].chunkData;
        List<BuildingInfo> missionBuildings = chunk.missionBuildings;

        int size = missionBuildings.Count;
        int ind = Random.Range(0, size);

        return missionBuildings[ind].pos;
    }

    public static void checkMissionTrigger(Vector2 pos) {
        if(mission != null) {
            if (!isStarted) {
                if (pos == mission.startLocation) {
                    isStarted = true;
                    timerUI.SetActive(true);
                    questUI.SetActive(true);
                    q = true;
                    beacon.transform.SetPositionAndRotation(new Vector3(mission.endLocation.x, 0, mission.endLocation.y), new Quaternion());
                    timer = 0;
                    Debug.Log(mission.estimatedTime);
                }
            }
            else {
                if(pos == mission.endLocation) {

                    float t = timer / mission.estimatedTime;
                    Debug.Log(timer);
                    Debug.Log(t);

                    if(t <= 1) {
                        //5 stars
                        playerInfo.Money += mission.reward;
                    }
                    else if (t <= 1.1) {
                        //4 stars
                        playerInfo.Money += .9f * mission.reward;
                    }
                    else if (t <= 1.2) {
                        //3 stars
                        playerInfo.Money += .8f * mission.reward;
                    }
                    else if (t <= 1.3) {
                        //2 stars
                        playerInfo.Money += .7f * mission.reward;
                    }
                    else {
                        //1 stars
                        playerInfo.Money += .6f * mission.reward;
                    }

                    isStarted = false;
                    timerUI.SetActive(false);
                    questUI.SetActive(false);
                    q = false;
                    mission = null;

                    createRandomMission();
                }
            }
        }
    }
}

