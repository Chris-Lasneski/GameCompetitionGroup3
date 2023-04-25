using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MissionController {
    
    static WorldRenderer world;

    static MissionDetails mission = null;
    static bool isStarted = false;

    public static void initMissions(WorldRenderer world) {
        MissionController.world = world;
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
                if(pos == mission.startLocation) isStarted = true;
            }
            else {
                if(pos == mission.endLocation) {
                    isStarted = false;
                    mission = null;
                    //TODO: on mission completed
                }
            }
        }
    }
}

public class MissionDetails {
    public Vector2 startLocation;
    public Vector2 endLocation;
}