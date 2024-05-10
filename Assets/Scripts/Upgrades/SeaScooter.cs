using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeaScooter", menuName = "Upgrades/SeaScooter", order = 0)]
public class SeaScooter : Upgrade
{
    public string upgradeName;
    public float speed;
    public int tier;
    public int tierOneCost;
    public int tierTwoCost;
    public int tierThreeCost;
    public Mesh mesh;
    public Sprite icon;
}
