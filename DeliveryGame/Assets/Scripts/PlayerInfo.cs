using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerInfo;
using static UpgradeUI;

public class PlayerInfo : MonoBehaviour
{
    // currently set for testing purposes will need to be changed for final value.
    public float Money = 20000f;
    public bool paused = false;

    [SerializeField] public GameObject sedan;
    [SerializeField] public GameObject hatchback;
    [SerializeField] public GameObject sports;
    [SerializeField] public GameObject muscle;
    [SerializeField] public GameObject suv;
    [SerializeField] public GameObject van;
    [SerializeField] public GameObject pickup;

    [SerializeField] public bool sedOwn = true;
    [SerializeField] public bool hatchOwn = false;
    [SerializeField] public bool sportsOwn = false;
    [SerializeField] public bool muscleOwn = false;
    [SerializeField] public bool suvOwn = false;
    [SerializeField] public bool vanOwn = false;
    [SerializeField] public bool pickupOwn = false;

    public GameObject currentCar;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        LawEnforcementController.playerInfo = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
