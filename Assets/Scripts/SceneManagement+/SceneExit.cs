//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : Interactable
{
    [SerializeField]
    public override void Interact()
    {
        SceneLoader.ExitScene();
    }
}
