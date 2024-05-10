using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : Interactable
{
    public Trash trash;

    public override void Interact()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.TrashCollected, this));

        //  gameObject.GetComponentInParent<TrashCollection>().RemoveTrash(this);
        Destroy(gameObject);
    }
}
