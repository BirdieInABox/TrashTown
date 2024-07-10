//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingItem : MonoBehaviour, IEventListener, IPointerClickHandler
{
    [SerializeField]
    private Upgrade upgrade;
    private bool isCraftable = true;

    private void OnEnable()
    {
        EventManager.MainStatic.AddListener(this);
        if (upgrade.isCrafted)
            isCraftable = false;
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.ItemCrafted)
        {
            if ((Upgrade)receivedEvent.Data == upgrade)
                LockCraft();
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isCraftable)
            OnSelect();
    }

    private void LockCraft()
    {
        upgrade.isCrafted = true;
        isCraftable = false;
    }

    //Called on click in crafting interface

    public void OnSelect()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingItemSelected, upgrade));
    }
}
