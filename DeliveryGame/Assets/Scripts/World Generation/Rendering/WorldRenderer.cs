using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class WorldRenderer : MonoBehaviour
{
    public GameObject[] intersectionPfb;
    public int[] intersectionWeights;
    public GameObject roadPfb;
    public GameObject emptyPfb;
    public GameObject[] buildingPfb;
    public int[] buildingWeights;
    public bool[] isMissionBuilding;

    public PlayerInfo player;

    public Dictionary<Vector2Int, Chunk> chunkList = new Dictionary<Vector2Int, Chunk>();
    private Rigidbody playerBody;
    Vector2Int chunkPos;

    // Start is called before the first frame update
    void Start() {

        Map.initMap(buildingWeights, intersectionWeights, isMissionBuilding);

        playerBody = player.currentCar.GetComponent<Rigidbody>();
        chunkPos = new Vector2Int(
            (int)playerBody.position.x / WorldGenerationConstants.chunkSize,
            (int)playerBody.position.y / WorldGenerationConstants.chunkSize
        );

        renderChunk(Map.getChunk(chunkPos.x, chunkPos.y));
        foreach (Vector2Int displacement in Map.directionToExternalIndex.Keys) {
            Vector2Int externChunk = chunkPos + displacement;
            renderChunk(Map.getChunk(externChunk.x, externChunk.y));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = playerBody.position;
        int posX = (int)pos.x - Math.mod((int)pos.x, WorldGenerationConstants.chunkSize);
        int posY = (int)pos.z - Math.mod((int)pos.z, WorldGenerationConstants.chunkSize);


        Vector2Int chpos = new Vector2Int(posX / WorldGenerationConstants.chunkSize, posY / WorldGenerationConstants.chunkSize);
        if (chpos != chunkPos) {

            List<Vector2Int> newChunks = new List<Vector2Int>();
            newChunks.Add(chpos);
            foreach (Vector2Int displacement in Map.directionToExternalIndex.Keys) {
                Vector2Int externChunk = chpos + displacement;
                newChunks.Add(externChunk);
            }

            List<Vector2Int> removalList = new List<Vector2Int>();

            foreach (Vector2Int p in chunkList.Keys) {
                if(!newChunks.Contains(p)) {
                    chunkList[p].delete();
                    removalList.Add(p);
                }
            }

            foreach(Vector2Int p in removalList) {
                chunkList.Remove(p);
            }

            chunkPos = chpos;
            foreach (Vector2Int p in newChunks) {
                if (!chunkList.ContainsKey(p)) {
                    renderChunk(Map.getChunk(p.x, p.y));
                }
            }
        }
    }




    #region Rendering

    private void renderChunk(ChunkData data) {
        Chunk chunk = new Chunk();

        chunk.chunkData = data;

        chunk.parent = Instantiate(emptyPfb, transform);

        //add intersections
        foreach (IntersectionInfo intersection in data.intersections) {
            chunk.intersectionBehaviors.Add(placeIntersection(intersection, chunk.parent.transform));
        }

        //add roads
        foreach (RoadInfo road in data.roads) {
            chunk.roadBehaviors.Add(placeRoad(road, chunk.parent.transform));
        }

        foreach (Vector2Int displacement in Map.directionToExternalIndex.Keys) {
            if (chunkList.ContainsKey(data.chunkPos + displacement)) {
                int directionId = Map.directionToExternalIndex[displacement];
                int extDirectionId = (directionId + 4) % 8;

                foreach (RoadInfo road in data.externalRoads[directionId]) {
                    RoadBehavior behavior = placeRoad(road, chunk.parent.transform);

                    chunk.externalRoadBehavior[directionId].Add(behavior);
                    chunkList[chunk.chunkData.chunkPos + displacement].externalRoadBehavior[extDirectionId].Add(behavior);
                }
            }
        }

        //add buildings
        foreach (BuildingInfo buildingInfo in data.buildings) {
            chunk.buildingBehavior.Add(placeBuilding(buildingInfo, chunk.parent.transform));
        }

        foreach (Vector2Int displacement in Map.directionToExternalIndex.Keys) {
            if (chunkList.ContainsKey(data.chunkPos + displacement)) {
                int directionId = Map.directionToExternalIndex[displacement];
                int extDirectionId = (directionId + 4) % 8;

                foreach (BuildingInfo building in data.externalBuildings[directionId]) {
                    BuildingBehavior behavior = placeBuilding(building, chunk.parent.transform);

                    chunk.externalBuildingBehavior[directionId].Add(behavior);
                    chunkList[chunk.chunkData.chunkPos + displacement].externalBuildingBehavior[extDirectionId].Add(behavior);
                }
            }
        }

        chunkList.Add(data.chunkPos, chunk);
    }

    //placing a road requires two nodes for placement.
    private RoadBehavior placeRoad(RoadInfo e, Transform t) {
        Vector3 pos = Between(e);
        Quaternion rot = Quaternion.LookRotation(new Vector3(e.b.x - e.a.x, 0, e.b.y - e.a.y));

        GameObject ret = Instantiate(roadPfb, pos, rot, t);

        return ret.AddComponent<RoadBehavior>();
    }

    private IntersectionBehavior placeIntersection(IntersectionInfo i, Transform t) {
        Vector3 pos = new Vector3(i.pos.x, 0, i.pos.y);

        GameObject ret = Instantiate(intersectionPfb[i.intersectionType], pos, new Quaternion(), t);

        return ret.AddComponent<IntersectionBehavior>();
    }

    public static Vector3 Between(RoadInfo e) {
        return new Vector3((e.a.x + e.b.x) / 2, 0, (e.a.y + e.b.y) / 2);
    }

    private BuildingBehavior placeBuilding(BuildingInfo b, Transform t) {
        Vector3 pos = new Vector3(b.pos.x, 0, b.pos.y);


        GameObject go = Instantiate(buildingPfb[b.buildingType], pos, Quaternion.Euler(0, b.rot, 0), t);

        BuildingBehavior ret;

        if(b.isMissionBuilding) ret = go.AddComponent<MissionBuildingBehavior>();
        else ret = go.AddComponent<BuildingBehavior>();

        ret.position = b.pos;

        return ret;
    }

    #endregion
}
