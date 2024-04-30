using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    AirBottle,
    SeaScooter,
    Backpack
}

[CreateAssetMenu(fileName = "CraftingItem", menuName = "CraftingItem", order = 0)]
public class CraftingItem : ScriptableObject
{
    public ItemType itemType;
    public int tierOneCost;
    public int tierTwoCost;
    public int tierThreeCost;
    public int tierFourCost;
    public Upgrade upgrade;
    public Upgrades upgrades;

    public void CraftItem()
    {
        upgrades.Upgrade(itemType, upgrade);
    }
}
