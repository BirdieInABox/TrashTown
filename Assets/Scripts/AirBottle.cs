using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AirBottle", menuName = "Upgrades/AirBottle", order = 0)]
public class AirBottle : ScriptableObject
{/*
    public AirBottle(Upgrade upgrade)
    {
        tier = upgrade.tier;
        bottleName = upgrade.bottleName;
        capacity = upgrade.capacity;
        mesh = upgrade.mesh;
    }*/

    public int tier;
    public string bottleName;
    public float capacity;
    public Mesh mesh;
}
