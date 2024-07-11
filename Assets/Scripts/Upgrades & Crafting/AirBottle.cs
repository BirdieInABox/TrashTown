//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirBottle", menuName = "Upgrades/AirBottle", order = 0)]
public class AirBottle : Upgrade
{
    //The name of the upgrade
    public string upgradeName;

    //the bottle's capacity in seconds
    public float capacity;

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
