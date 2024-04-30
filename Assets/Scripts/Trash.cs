using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trash", menuName = "Trash", order = 0)]
public class Trash : ScriptableObject
{
    public int trashID;
    public int trashTier;
    public string trashName;
    public string trashTierName;
    public GameObject prefab;
}
