//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{
    [SerializeField] //The crafting UI that has to be displayed
    private GameObject craftingUI;

    public override void Interact()
    {
        //Toggle controls
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingToggled));
        //Activate UI
        craftingUI.SetActive(true);
    }
}
