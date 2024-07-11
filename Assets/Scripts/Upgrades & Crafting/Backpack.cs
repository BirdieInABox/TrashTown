//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Upgrades/Backpack", order = 0)]
public class Backpack : Upgrade
{
    //The name of the upgrade
    public string upgradeName;

    //the maximum amount of trash the player can carry with this backpack
    public int size;

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
