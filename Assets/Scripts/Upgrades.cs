using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public AirBottle airBottle;
    public SeaScooter scooter;
    public Backpack backpack;

    public float GetCapacity()
    {
        return airBottle.capacity;
    }

    public float GetSpeed()
    {
        return scooter.speed;
    }

    public int GetSize()
    {
        return backpack.size;
    }

    public void Upgrade(ItemType itemType, Upgrade upgradeItem)
    {
        if (itemType.ToString() == "AirBottle")
        {
            // airBottle = new AirBottle(upgradeItem);
        }
        else if (itemType.ToString() == "SeaScooter")
        {
            //  scooter = new SeaScooter(upgradeItem);
        }
        else if (itemType.ToString() == "Backpack")
        {
            //  backpack = new Backpack(upgradeItem);
        }
    }
}
