using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashObject : Interactable
{
    public Trash trash;

    public override void Interact()
    {
        Debug.Log("Trash");
    }
}
