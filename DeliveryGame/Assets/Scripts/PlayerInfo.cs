using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;
using static PlayerInfo;
using static UpgradeUI;

public class PlayerInfo : MonoBehaviour
{
    // currently set for testing purposes will need to be changed for final value.
    public float Money = 20000f;
    public bool paused = false;

    // should be able to find items list this way
    //private UpgradeUI upgradeUI;

    //public List<UpgradeItem> items;

    //public class UpgradeItem
    //{
    //    string name;
    //    int cost;
    //    bool bought;
    //    public UpgradeItem(string name, int cost, bool bought)
    //    {
    //        this.name = name;
    //        this.cost = cost;
    //        this.bought = bought;
    //    }

    //    public string getName()
    //    {
    //        return this.name;
    //    }

    //    public int getCost() { return this.cost; }

    //    public bool isBought()
    //    {
    //        return this.bought;
    //    }
    //    public void Buy() {    this.bought = true; }
    //}
    //public List<UpgradeItem> items = new List<UpgradeItem>();



    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
        LawEnforcementController.playerInfo = this;

        //upgradeUI = GetComponent<UpgradeUI>();
        //items = upgradeUI.items;

        //if (items == null)
        //{
        //    items.Add(new UpgradeItem("wheel1", 0, true));
        //    items.Add(new UpgradeItem("wheel2", 200, false));
        //    items.Add(new UpgradeItem("wheel3", 500, false));
        //    items.Add(new UpgradeItem("wheel4", 750, false));
        //    items.Add(new UpgradeItem("engine1", 0, true));
        //    items.Add(new UpgradeItem("engine2", 300, false));
        //    items.Add(new UpgradeItem("engine3", 600, false));
        //    items.Add(new UpgradeItem("engine4", 1000, false));
        //}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
