using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trash", menuName = "Trash", order = 0)]
public class Trash : Upgrade
{
    public string trashName;
    public int trashTier;
    public int trashWorth;
    public GameObject prefab;
}
