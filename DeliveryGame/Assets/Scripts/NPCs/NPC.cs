using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NPC : MonoBehaviour
{
	public NavMeshAgent agent;

	public NavMeshSurface surface;
	public Vector3 destination;
	private float timer=0;
    // Start is called before the first frame update
    void Start()
    {
		agent.updatePosition = true;


		//agent.SetDestination(new Vector3(100,100,100));
		//surface.BuildNavMesh();
		
	
    }

	void Update () {
		GameObject player=GameObject.Find("Player");
		if (Vector3.Distance(player.transform.position, transform.position)>900){
			Destroy(transform.parent.gameObject);
			return;
		}
		if (agent.remainingDistance<1 && agent.remainingDistance>0){
		//Debug.Log("at destination!"+agent.remainingDistance);
		Destroy(transform.parent.gameObject);
		}
		if (agent.velocity.magnitude<.1){
		timer+=Time.deltaTime;
		if (timer>5){
		
		
		timer=0;
		}
		}
		
		destination=agent.destination;
	}
}