//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : ScriptableObject
{
    [HideInInspector] //if the item has already been crafted
    public bool isCrafted = false;
}
