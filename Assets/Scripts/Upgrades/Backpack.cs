//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Upgrades/Backpack", order = 0)]
public class Backpack : Upgrade
{
    public string upgradeName;

    public int size;

    public int tier;

    public int tierOneCost;

    public int tierTwoCost;

    public int tierThreeCost;

    public Mesh mesh;

    public Sprite icon;
}
