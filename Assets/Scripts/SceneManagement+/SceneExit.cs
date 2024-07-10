//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : Interactable
{
    public override void Interact()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.SaveGame));
        SceneLoader.SwitchScene();
    }
}
