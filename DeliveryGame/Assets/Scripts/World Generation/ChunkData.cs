using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Chunk {
    public ChunkData chunkData;

    public GameObject parent;
    public List<IntersectionBehavior> intersectionBehaviors = new List<IntersectionBehavior>();
    public List<RoadBehavior> roadBehaviors = new List<RoadBehavior>();
    public List<RoadBehavior>[] externalRoadBehavior = new List<RoadBehavior>[8];

    public List<BuildingBehavior> buildingBehavior = new List<BuildingBehavior>();
    public List<BuildingBehavior>[] externalBuildingBehavior = new List<BuildingBehavior>[8];

    public Chunk() {
        for (int i = 0; i < 8; i++) {
            externalRoadBehavior[i] = new List<RoadBehavior>();
        }
        for (int i = 0; i < 8; i++) {
            externalBuildingBehavior[i] = new List<BuildingBehavior>();
        }
    }

    public void delete() {
        MonoBehaviour.Destroy(parent);
    }
}

public class ChunkData {
    public Vector2Int chunkPos { get; private set; } // the center of the chunk is at chunkPos * chunkSize.

    public List<IntersectionInfo> intersections { get; private set; } // list of intersections in world units
    public List<RoadInfo> roads { get; private set; } // list of roads. roads are straight lines between two intersections
    public List<BuildingInfo> buildings { get; private set; } //list of buildings
    public List<BuildingInfo> missionBuildings { get; private set; } //list of buildings

    public List<RoadInfo>[] externalRoads { get; private set; }// list of roads going outside of the chunk in world units. roads are straight lines between two intersections
    public List<BuildingInfo>[] externalBuildings { get; private set; } //list of buildings on the border of a chunk


    public ChunkData(
        Vector2Int chunkPos, 
        List<IntersectionInfo> intersections, 
        List<RoadInfo> roads, 
        List<RoadInfo>[] externalRoads,
        List<BuildingInfo> buildings,
        List<BuildingInfo>[] externalBuildings,
        List<BuildingInfo> missionBuildings

    ) {
        this.chunkPos = chunkPos;
        this.intersections = intersections;
        this.roads = roads;
        this.externalRoads = externalRoads;
        this.buildings = buildings;
        this.externalBuildings = externalBuildings;
        this.missionBuildings = missionBuildings;
    }

}

public class RoadInfo {
    public Vector2 a { get; private set; }
    public Vector2 b { get; private set; }

    public RoadInfo(Vector2 a, Vector2 b) {
        this.a = a;
        this.b = b;
    }
}

public class BuildingInfo {
    public Vector2 pos;
    public float rot;
    public int buildingType;
    public bool isMissionBuilding;
}

public class IntersectionInfo {
    public Vector2 pos;
    public int intersectionType;
    public bool isOpposite;
}

//public class IntersectionInfo {
//    public Vector2 pos;
//    public int intersectionType;
//}

