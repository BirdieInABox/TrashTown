//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for interactables
public class Interactable : MonoBehaviour
{
    /// <summary>
    /// Called upon interaction by the player
    /// </summary>
    public virtual void Interact() { }
}
