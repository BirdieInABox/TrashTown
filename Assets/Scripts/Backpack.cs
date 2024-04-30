using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Backpack", menuName = "Upgrades/Backpack", order = 0)]
public class Backpack : Upgrade
{
    /*
    public Backpack(Upgrade upgrade)
    {
        tier = upgrade.tier;
        bottleName = upgrade.backpackName;
        capacity = upgrade.size;
        mesh = upgrade.mesh;
    }
*/
    public int tier;
    public string backpackName;
    public int size;
    public Mesh mesh;
}
