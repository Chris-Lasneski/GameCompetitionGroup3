using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class nav : MonoBehaviour {
    public NavMeshSurface surface;
    public GameObject npcForward;
    public GameObject npcBackward;
    public WorldRenderer world;
    public PlayerInfo playerInfo;

    public int initialNPCs = 30;
    int frame = 0;

    // Start is called before the first frame update
    void Start() {
        surface.BuildNavMesh();

        for (int i = 0; i < initialNPCs; i++) {
            generateNPC();
        }
    }

    void generateNPC() {

        int rand1, rand2, rand3, rand4;
        int roadSize = (int)WorldGenerationConstants.roadLength;
        GameObject newNpc;
        GameObject playerGO = playerInfo.currentCar;


        int playerIntersectionX = (((int)playerGO.transform.position.x) / roadSize) * roadSize;
        int playerIntersectionZ = (((int)playerGO.transform.position.z) / roadSize) * roadSize;
        int newX, newZ;
        NavMeshAgent npcAgent;

        //there are two types of NPCs, "forward" NPCs and "backward" NPCs.
        //the forward NPCs only use the right lane, and the backward NPCs only the left.
        //Each forward NPC's destination is some number of blocks forward, so they can all always face the same direction.

        rand1 = Random.Range(1, 5);
        rand2 = Random.Range(1, 5);
        rand3 = Random.Range(1, 5);
        rand4 = Random.Range(1, 5);
        newX = playerIntersectionX + roadSize * rand1 + 3 - 3 * roadSize;
        newZ = playerIntersectionZ + roadSize * rand2 + Random.Range(20, 70) - 3 * roadSize;
        bool forward = Random.value > 0.5;
        if (forward) {
            newNpc = Instantiate(npcForward, new Vector3(newX, 0, newZ), Quaternion.identity, transform);
        }
        else {
            newNpc = Instantiate(npcBackward, new Vector3(newX, 0, newZ), Quaternion.identity, transform);
            newNpc.transform.Rotate(0, 180, 0);
        }
        newNpc.SetActive(true);
        npcAgent = newNpc.transform.GetChild(0).GetComponent<NavMeshAgent>();
        if (forward) {
            npcAgent.SetDestination(new Vector3(newX + rand3 * roadSize, 0, newZ + rand4 * roadSize));
        }
        else {
            npcAgent.SetDestination(new Vector3(newX - rand3 * roadSize, 0, newZ - rand4 * roadSize));
        }
    }

    // Update is called once per frame
    void Update() {
        if (frame % 6 == 0) {
            generateNPC();
        }
        frame++;
    }
}
