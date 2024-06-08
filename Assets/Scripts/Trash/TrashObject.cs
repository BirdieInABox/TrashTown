//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MessagePack;

[MessagePackObject]
public class TrashObject : Interactable
{
    [Key(0)]
    public Trash trash;

    public override void Interact()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.TrashCollected, this));

        //  gameObject.GetComponentInParent<TrashCollection>().RemoveTrash(this);
        Destroy(gameObject);
    }
}
