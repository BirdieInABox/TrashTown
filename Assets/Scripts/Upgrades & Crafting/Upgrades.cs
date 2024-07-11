//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour, IEventListener
{
    public AirBottle airBottle; //the player's air bottle
    public SeaScooter scooter; //the player's sea scooter
    public Backpack backpack; //the player's backpack

    //returns the bottle's capacity
    public float GetCapacity()
    {
        return airBottle.capacity;
    }

    //returns the scooter's speed
    public float GetSpeed()
    {
        return scooter.speed;
    }

    //returns the backpack's size
    public int GetSize()
    {
        return backpack.size;
    }

    private void Start()
    {
        //Add this as listener to the event system
        EventManager.MainStatic.AddListener(this);
    }

    /// <summary>
    /// Updates an upgrade with a new upgrade
    /// </summary>
    /// <param name="upgradeItem">the new upgrade as generic Upgrade</param>
    public void Upgrade(Upgrade upgradeItem)
    {
        //if the upgrade is of type AirBottle
        if (upgradeItem is AirBottle)
        {
            //Update the player's airBottle with the new upgrade
            airBottle = (upgradeItem as AirBottle);
        } //if the upgrade is of type SeaScooter
        else if (upgradeItem is SeaScooter)
        {
            //Update the player's scooter with the new upgrade
            scooter = (upgradeItem as SeaScooter);
        } //if the upgrade is of type Backpack
        else if (upgradeItem is Backpack)
        {
            //Update the player's backpack with the new upgrade
            backpack = (upgradeItem as Backpack);
        }
    }

    /// <summary>
    /// Called upon EventSystem sending an event
    /// </summary>
    /// <param name="receivedEvent">the received event, including type and content</param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //If the event received is of type ItemCrafted
        if (receivedEvent.Type == EventType.ItemCrafted)
            //update the player's upgrade using the event's payload
            Upgrade((Upgrade)receivedEvent.Data);
    }
}
