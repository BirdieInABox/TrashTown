using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingItem : MonoBehaviour, IEventListener, IPointerClickHandler
{
    [SerializeField]
    private Upgrade upgrade;

    void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.ItemCrafted)
        {
            if ((Upgrade)receivedEvent.Data == upgrade)

                OnCraft();
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnSelect();
    }

    public void OnCraft()
    {
        //Lock this craft
    }

    //Called on click in crafting interface

    public void OnSelect()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingItemSelected, upgrade));
    }
}
