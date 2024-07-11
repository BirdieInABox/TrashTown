//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingItem : MonoBehaviour, IEventListener, IPointerClickHandler
{
    [SerializeField] //the upgrade associated with this object
    private Upgrade upgrade;

    //if the item is still craftable
    private bool isCraftable = true;

    /// <summary>
    /// Called upon enabling the object for the first time
    /// This isn't a "Start" function, as the UI starts disabled
    /// </summary>
    private void OnEnable()
    {
        //add this as listener to the event system
        EventManager.MainStatic.AddListener(this);
        //check if the item has been crafted already
        if (upgrade.isCrafted)
            isCraftable = false;
    }

    /// <summary>
    /// Called upon EventSystem sending an event
    /// </summary>
    /// <param name="receivedEvent">the received event, including type and content</param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //If the received event is of type "ItemCrafted"
        if (receivedEvent.Type == EventType.ItemCrafted)
        {
            //if the item crafted is the same as the one associated with this object
            if ((Upgrade)receivedEvent.Data == upgrade)
                //Lock this craft
                LockCraft();
        }
    }

    /// <summary>
    /// Called upon the player clicking on this object
    /// </summary>
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //if the item is still craftable
        if (isCraftable)
            //Select it
            OnSelect();
    }

    /// <summary>
    /// Locks the craft and sets the item to uncraftable
    /// </summary>
    private void LockCraft()
    {
        upgrade.isCrafted = true;
        isCraftable = false;
    }

    /// <summary>
    /// Tells the overview to display the stats of the upgrade associated with this object
    /// </summary>
    public void OnSelect()
    {
        //Sends an event of type "CraftingItemSelected" to the event system with this object's upgrade as payload
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingItemSelected, upgrade));
    }
}
