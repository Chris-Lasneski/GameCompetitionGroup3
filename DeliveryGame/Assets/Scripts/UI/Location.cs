using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Location : MonoBehaviour
{

    public Text time;
    public PlayerInfo player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Current Location: " + player.currentCar.transform.position.x.ToString("0.00") + ", " + player.currentCar.transform.position.z.ToString("0.00");

    }
}
