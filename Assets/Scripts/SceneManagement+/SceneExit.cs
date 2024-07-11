//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : Interactable
{
    /// <summary>
    /// Called when player interacts with this object
    /// </summary>
    public override void Interact()
    {
        //Tell the SavingManager to save the game via an event sent through the event system
        EventManager.MainStatic.FireEvent(new EventData(EventType.SaveGame));
        //Tell SceneLoader to switch scenes
        SceneLoader.SwitchScene();
    }
}
