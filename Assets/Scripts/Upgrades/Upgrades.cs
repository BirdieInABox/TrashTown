//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour, IEventListener
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

    private void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void Upgrade(Upgrade upgradeItem)
    {
        if (upgradeItem is AirBottle)
        {
            airBottle = (upgradeItem as AirBottle);
        }
        else if (upgradeItem is SeaScooter)
        {
            scooter = (upgradeItem as SeaScooter);
        }
        else if (upgradeItem is Backpack)
        {
            backpack = (upgradeItem as Backpack);
        }
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.ItemCrafted)
            Upgrade((Upgrade)receivedEvent.Data);
    }
}
