using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
public class nav : MonoBehaviour
{
	public NavMeshSurface surface;
	public GameObject npcForward;
	public GameObject npcBackward;
	public WorldRenderer world;
	public PlayerInfo player;

	public int initialNPCs=50;
	
	int frame=0;
    // Start is called before the first frame update
    void Start()
    {
		surface.BuildNavMesh();
	
		for (int i=0; i<initialNPCs; i++){
			generateNPC();
		}
			
	

    }
	private bool rebuilding=false;
	void foo(){
		
		if (!rebuilding){
			rebuilding=true;
			
			StartCoroutine(rebuild());
		}
	}
	IEnumerator rebuild(){
		
				surface.UpdateNavMesh(surface.navMeshData);
				Debug.Log("Finished Rebuilding");
				yield return null;
				rebuilding=false;
	}
	void generateNPC(){
		
		int rand1, rand2, rand3, rand4;
		int roadSize = (int)WorldGenerationConstants.roadLength;
        GameObject newNpc;
		GameObject playerRB = player.currentCar;//GameObject.Find("Player");


        int playerIntersectionX=(((int)playerRB.transform.position.x)/ roadSize) * roadSize;
		int playerIntersectionZ=(((int)playerRB.transform.position.z)/ roadSize) * roadSize;
		int newX,newZ;
		NavMeshAgent a;
		//Debug.Log(randomPos1+""+hit1.position+""+randomPos2+""+hit2.position);
	//Ok, so I took some liberties to simplify the logic:
	//there are two types of NPCs, "forward" NPCs and "backward" NPCs.
	//the forward NPCs only use the right lane, and the backward NPCs only the left.
	//Each forward NPC's destination is some number of blocks forward, so they can all always face the same direction.
		
			//if (Random.value > 0.5){
			//newNpc=Instantiate(npcForward, hit1.position, Quaternion.identity);
	
			rand1=Random.Range(1, 5);
			rand2=Random.Range(1, 5);
			rand3=Random.Range(1, 5);
			rand4=Random.Range(1, 5);
			newX=playerIntersectionX + roadSize * rand1 + 3 - 3 * roadSize;
			newZ=playerIntersectionZ + roadSize * rand2 + Random.Range(20,70) - 3 * roadSize;
			bool forward=Random.value>0.5;
			if (forward){
				newNpc=Instantiate(npcForward,new Vector3(newX,0,newZ), Quaternion.identity);

			}
			else{
				newNpc=Instantiate(npcBackward,new Vector3(newX,0,newZ), Quaternion.identity);
				newNpc.transform.Rotate(0,180,0);

				
			}			
			newNpc.SetActive(true);
			a=newNpc.transform.GetChild(0).GetComponent<NavMeshAgent>();
			if (forward){
				a.SetDestination(new Vector3(newX + rand3 * roadSize, 0, newZ + rand4 * roadSize));
			}
			else{
				a.SetDestination(new Vector3(newX - rand3 * roadSize, 0, newZ  -rand4 * roadSize));
			}
				
			//newNpc.GetComponent<NavMeshAgent>().SetDestination(new Vector3((rand1+rand3)*90,0,(rand2+rand4)*90));
			
			//Debug.Log(newNpc.GetComponent<NavMeshAgent>().destination);

	
			
			//newNpc.GetComponent<NavMeshAgent>().SetDestination(new Vector3((rand1+rand3)*90,0,(rand2+rand4)*90));
		//	newNpc.transform.GetChild(0).GetComponent<NavMeshAgent>().SetDestination(new Vector3(Mathf.Max(rand1-rand3,0)*90,0,Mathf.Max(rand2-rand4,0)*90));
			//Debug.Log(newNpc.GetComponent<NavMeshAgent>().destination);

			//newNpc.GetComponent<NavMeshAgent>().SetDestination(hit2.position);
		
	}
    // Update is called once per frame
    void Update()
    {
		
				//foo();
if (frame%6==0){

	generateNPC();
			/*
			}
			else {
					
			newNpc=Instantiate(npcBackward,hit1.position, Quaternion.identity);
			newNpc.transform.GetChild(0).GetComponent<NPC>().destination=hit2.position;
			
			
			}
			*/
		}
		frame++;
    }
}
