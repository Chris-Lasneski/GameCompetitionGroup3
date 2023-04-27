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

    public static void fixedUpdate() {
        timer += Time.deltaTime;
    }

    public static void initMissions(WorldRenderer world, GameObject beaconPfb, PlayerInfo playerInfo) {
        MissionController.world = world;
        MissionController.playerInfo = playerInfo;

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
                    beacon.transform.SetPositionAndRotation(new Vector3(mission.endLocation.x, 0, mission.endLocation.y), new Quaternion());
                    timer = 0;
                }
            }
            else {
                if(pos == mission.endLocation) {

                    float t = timer / mission.estimatedTime;

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
                    mission = null;

                    createRandomMission();
                }
            }
        }
    }
}

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