using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{

    AudioSource playerEngine;
    private Rigidbody car;
    public PlayerInfo playerInfo;
    private float velocity;
    private float maxFirst = 25;
    private float gearStep = 15;

    // Start is called before the first frame update
    void Start()
    {
        playerEngine = GetComponent<AudioSource>();
        car = playerInfo.currentCar.GetComponent<Rigidbody>();
        playerEngine.pitch = 1;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = car.velocity.magnitude * 2.237f;

        if (velocity < maxFirst)
            playerEngine.pitch = velocity/maxFirst + 1;
        else
            playerEngine.pitch = (((velocity - maxFirst) % gearStep) / gearStep) + 2;
    }
}
