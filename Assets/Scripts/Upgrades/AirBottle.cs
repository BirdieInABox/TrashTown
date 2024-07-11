//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirBottle", menuName = "Upgrades/AirBottle", order = 0)]
public class AirBottle : Upgrade
{
    public string upgradeName;

    public float capacity;

    public int tier;

    public int tierOneCost;

    public int tierTwoCost;

    public int tierThreeCost;
    public int tierFourCost;

    public Mesh mesh;

    public Sprite icon;
}
