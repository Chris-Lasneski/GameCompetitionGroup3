using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    //[SerializeField]
    //private Transform scrollViewContent;

    //[SerializeField]
    //private GameObject scrollViewContentPrefab;

    //[SerializeField]
    //private List<Sprite> sprites;

    [System.Serializable]
    public class Upgrades
    {
        public string name;
        public int cost;
        public int modifier;
        public bool bought;
        public Sprite image;
        [HideInInspector] public GameObject itemRef;
    }

    public Upgrades[] upgrades;

    public GameObject Upgradeui;
    public Transform UpgradeShopContent;
    public GameObject UpgradeShopPrefab;

    public PlayerInfo player;
    public CarController carController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Upgrade";

        foreach (Upgrades upgrade in upgrades)
        {
            GameObject item = Instantiate(UpgradeShopPrefab, UpgradeShopContent);
            upgrade.itemRef = item;

            foreach(Transform child in item.transform)
            {
                if(child.gameObject.name == "Cost")
                {
                    child.gameObject.GetComponent<Text>().text = "$" + upgrade.cost.ToString();
                }
                else if(child.gameObject.name == "Image")
                {
                    child.gameObject.GetComponent<Image>().sprite = upgrade.image;
                }
            }

            item.GetComponent<Button>().onClick.AddListener(() => { buyUpgrade(upgrade); });
        }

        //foreach (Sprite sprite in sprites)
        //{
        //    GameObject newSprite = Instantiate(scrollViewContentPrefab, scrollViewContent);

        //}
    }


    //handles when player purchases an upgrade
    public void buyUpgrade(Upgrades upgrade)
    {
        if (player.Money >= upgrade.cost && upgrade.bought == false)
        {
            player.Money -= upgrade.cost;
            upgrade.bought = true;
            applyUpgrade(upgrade);
        }
        // to check if player already owns the upgrade. allows the player to downgrade to a previous state if they choose. idk why they would though mostly just to allow them to equip better version if they buy a lower level one after a higher level tier.
        else if (upgrade.bought)
        {
            applyUpgrade(upgrade);
        }
    }

    //applies the upgrades, need to work on logic later
    public void applyUpgrade(Upgrades upgrade)
    {
        switch (upgrade.name)
        {
            case "Wheel1": carController.brakeTorqueModifier = upgrade.modifier; break;
            case "Wheel2": carController.brakeTorqueModifier = upgrade.modifier; break;
            case "Wheel3": carController.brakeTorqueModifier = upgrade.modifier; break;
            case "Wheel4": carController.brakeTorqueModifier = upgrade.modifier; break;
            case "Engine1": carController.maxSpeedModifier = upgrade.modifier; break;
            case "Engine2": carController.maxSpeedModifier = upgrade.modifier; break;
            case "Engine3": carController.maxSpeedModifier = upgrade.modifier; break;
            case "Engine4": carController.maxSpeedModifier = upgrade.modifier; break;
            default: Debug.Log("No Matching Upgrade"); break;
        }
    }
}