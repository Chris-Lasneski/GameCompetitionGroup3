using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCars : MonoBehaviour
{
    public PlayerInfo player;

    private GameObject car;

    public Button SedanB;
    public Button HatchB;
    public Button SportsB;
    public Button MuscleB;
    public Button SUVB;
    public Button VanB;
    public Button PickupB;

    // Start is called before the first frame update
    void Start()
    {
        car = player.currentCar;

        SedanB.onClick.AddListener(delegate { buyCar(0); });
        HatchB.onClick.AddListener(delegate { buyCar(1); });
        SportsB.onClick.AddListener(delegate { buyCar(2); });
        MuscleB.onClick.AddListener(delegate { buyCar(3); });
        SUVB.onClick.AddListener(delegate { buyCar(4); });
        VanB.onClick.AddListener(delegate { buyCar(5); });
        PickupB.onClick.AddListener(delegate { buyCar(6); });
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void buyCar(int i)
    {
        switch(i)
        {
            // default car, no need to do calcs, just switch to changing the car
            case 0:
                {
                    switchCar(player.sedan);
                    break;
                }
            case 1:
                {
                    if(player.Money >= 2500 && !player.hatchOwn)
                    {
                        player.Money -= 2500;
                        player.hatchOwn = !player.hatchOwn;
                        switchCar(player.hatchback);
                    }
                    else if(player.hatchOwn) { switchCar(player.hatchback); }
                    break;
                }
            case 2:
                {
                    if(player.Money >= 6500 && !player.sportsOwn)
                    {
                        player.Money -= 6500;
                        player.sportsOwn = !player.sportsOwn;
                        switchCar(player.sports);
                    }
                    else if(player.sportsOwn) { switchCar(player.sports); }
                    break;
                }
            case 3:
                {
                    if(player.Money >= 4500 && !player.muscleOwn)
                    {
                        player.Money -= 4500;
                        player.muscleOwn = !player.muscleOwn;
                        switchCar(player.muscle);
                    }
                    else if( player.muscleOwn) { switchCar(player.muscle); }
                    break;
                }
            case 4:
                {
                    if(player.Money >= 4000 && !player.suvOwn)
                    {
                        player.Money -= 4000;
                        player.suvOwn = !player.suvOwn;
                        switchCar(player.suv);
                    }
                    else if (player.suvOwn) { switchCar(player.suv); }
                    break;
                }
            case 5:
                {
                    if (player.Money >= 3750 && !player.vanOwn)
                    {
                        player.Money -= 3750;
                        player.vanOwn = !player.vanOwn;
                        switchCar(player.van);
                    }
                    else if (player.vanOwn) { switchCar(player.van); }
                    break;
                }
            case 6:
                {
                    if(player.Money >= 4250 && player.pickupOwn)
                    {
                        player.Money -= 4250;
                        player.pickupOwn = !player.pickupOwn;
                        switchCar(player.pickup);
                    }
                    else if (player.pickupOwn) { switchCar(player.pickup); }
                    break;
                }
            default:
                {
                    Debug.Log("How?"); break;
                }
        }
    }

    public void switchCar(GameObject v)
    {
        // should set current car to inactive, switch current car to new car and set to active
        player.currentCar.SetActive(false);
        // give new car the previous cars position and rotation
        v.transform.position = player.currentCar.transform.position;
        v.transform.rotation = player.currentCar.transform.rotation;
        player.currentCar = v;
        player.currentCar.SetActive(true);
    }
}
