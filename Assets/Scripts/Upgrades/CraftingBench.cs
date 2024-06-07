//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{
    [SerializeField]
    private GameObject craftingUI;

    public override void Interact()
    {
        craftingUI.SetActive(true);
    }
}
