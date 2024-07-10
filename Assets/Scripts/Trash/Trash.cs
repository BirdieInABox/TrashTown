//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trash", menuName = "Trash", order = 0)]
public class Trash : ScriptableObject
{
    public int trashTier;
    public int trashWorth;
}
