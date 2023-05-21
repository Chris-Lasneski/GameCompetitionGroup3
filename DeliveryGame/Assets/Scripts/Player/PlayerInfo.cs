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
    public bool subMenu = false;

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
        // moving updating other cars here, should help reduce some lag. not a good solution but it'll work for now.
        if (currentCar != sedan)
        {
            sedan.transform.position = currentCar.transform.position;
            sedan.transform.rotation = currentCar.transform.rotation;
        }

        if (currentCar != hatchback)
        {
            hatchback.transform.position = currentCar.transform.position;
            hatchback.transform.rotation = currentCar.transform.rotation;
        }

        if (currentCar != pickup)
        {
            pickup.transform.position = currentCar.transform.position;
            pickup.transform.rotation = currentCar.transform.rotation;
        }

        if (currentCar != sports)
        {
            sports.transform.position = currentCar.transform.position;
            sports.transform.rotation = currentCar.transform.rotation;
        }

        if (currentCar != muscle)
        {
            muscle.transform.position = currentCar.transform.position;
            muscle.transform.rotation = currentCar.transform.rotation;
        }

        if (currentCar != suv)
        {
            suv.transform.position = currentCar.transform.position;
            suv.transform.rotation= currentCar.transform.rotation;
        }

        if (currentCar != van)
        {
            van.transform.position = currentCar.transform.position;
            van.transform.rotation = currentCar.transform.rotation;
        }
    }

    public void switchSubMenu()
    {
        subMenu = !subMenu;
    }
}
