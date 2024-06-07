//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
[CreateAssetMenu(fileName = "AirBottle", menuName = "Upgrades/AirBottle", order = 0)]
public class AirBottle : Upgrade
{
    [Key(0)]
    public string upgradeName;

    [Key(1)]
    public float capacity;

    [Key(2)]
    public int tier;

    [Key(3)]
    public int tierOneCost;

    [Key(4)]
    public int tierTwoCost;

    [Key(5)]
    public int tierThreeCost;

    [Key(6)]
    public Mesh mesh;

    [Key(7)]
    public Sprite icon;
}
