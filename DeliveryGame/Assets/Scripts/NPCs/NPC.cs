using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class NPC : MonoBehaviour {
    public NavMeshAgent agent;
    public PlayerInfo playerInfo;
    public Vector3 destination;
    private float timer = 0;
    // Start is called before the first frame update
    void Start() {
        agent.updatePosition = true;
        playerInfo = GameObject.Find("Player").GetComponent<PlayerInfo>();
    }

    void Update() {
        GameObject playerGO = playerInfo.currentCar;
        if (Vector3.Distance(playerGO.transform.position, transform.position) > 400) {
            Destroy(transform.parent.gameObject);
            return;
        }
        if (agent.remainingDistance < 1 && agent.remainingDistance > 0) {
            Destroy(transform.parent.gameObject);
        }
        if (agent.velocity.magnitude < .1) {
            timer += Time.deltaTime;
            if (timer > 5) {
                timer = 0;
            }
        }

        destination = agent.destination;
    }
}