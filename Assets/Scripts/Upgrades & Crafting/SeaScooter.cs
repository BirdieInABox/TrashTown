//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaScooter", menuName = "Upgrades/SeaScooter", order = 0)]
public class SeaScooter : Upgrade
{
    //The name of the upgrade
    public string upgradeName;

    //the sprint speed of the scooter
    public float speed;

    //the upgrade tier
    public int tier;

   //the amount of tier 1 trash needed to craft this
    public int tierOneCost;

    //the amount of tier 2 trash needed to craft this
    public int tierTwoCost;

    //the amount of tier 3 trash needed to craft this
    public int tierThreeCost;
    
    //the amount of tier 4 trash needed to craft this
    public int tierFourCost;

    //the mesh of the upgrade
    public Mesh mesh;

    //the icon of the upgrade
    public Sprite icon;
}
