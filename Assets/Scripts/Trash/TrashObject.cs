//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : Interactable
{
    //The associated trash
    public Trash trash;

    /// <summary>
    /// called upon player interaction
    /// </summary>
    public override void Interact()
    {
        //Sends an event to the event system with this object as payload
        EventManager.MainStatic.FireEvent(new EventData(EventType.TrashCollected, this));
        //Destroys this object
        Destroy(gameObject);
    }
}
