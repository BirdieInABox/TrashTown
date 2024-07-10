//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour, IEventListener
{
    public int numOfTiers = 3;
    private int[] resourcesAmounts;
    private TMP_Text[] counterTexts;

    [SerializeField]
    private Upgrades upgrades;
    private int maxAmount;

    void Awake()
    {
        maxAmount = upgrades.GetSize();
        resourcesAmounts = new int[numOfTiers];
        counterTexts = new TMP_Text[numOfTiers];
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i < counterTexts.Length)
                counterTexts[i] = child.GetComponent<TMP_Text>();
            i++;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.TrashCollected)
            IncreaseResource((TrashObject)receivedEvent.Data);
        else if (receivedEvent.Type == EventType.ItemCrafted)
        {
            Upgrade upgrade = receivedEvent.Data as Upgrade;
            if (upgrade is AirBottle)
            {
                DecreaseResourceOnCraft(upgrade as AirBottle);
            }
            else if (upgrade is SeaScooter)
            {
                DecreaseResourceOnCraft(upgrade as SeaScooter);
            }
            else if (upgrade is Backpack)
            {
                DecreaseResourceOnCraft(upgrade as Backpack);
                maxAmount = (upgrade as Backpack).size;
            }
        }
    }

    private void DecreaseResourceOnCraft(AirBottle airbottle)
    {
        DecreaseResource(airbottle.tierOneCost, 1);
        DecreaseResource(airbottle.tierTwoCost, 2);
        DecreaseResource(airbottle.tierThreeCost, 3);
    }

    private void DecreaseResourceOnCraft(SeaScooter scooter)
    {
        DecreaseResource(scooter.tierOneCost, 1);
        DecreaseResource(scooter.tierTwoCost, 2);
        DecreaseResource(scooter.tierThreeCost, 3);
    }

    private void DecreaseResourceOnCraft(Backpack backpack)
    {
        DecreaseResource(backpack.tierOneCost, 1);
        DecreaseResource(backpack.tierTwoCost, 2);
        DecreaseResource(backpack.tierThreeCost, 3);
    }

    public void IncreaseResource(TrashObject obj)
    {
        int amount = obj.trash.trashWorth;
        int tierID = obj.trash.trashTier;
        if (tierID <= resourcesAmounts.Length && tierID > 0)
        {
            if (resourcesAmounts[tierID - 1] + amount > maxAmount)
            {
                resourcesAmounts[tierID - 1] = maxAmount;
            }
            else
            {
                resourcesAmounts[tierID - 1] += amount;
            }
            counterTexts[tierID - 1].text = resourcesAmounts[tierID - 1].ToString();
        }
        else
        {
            Debug.Log("Invalid TierID: " + tierID);
        }
    }

    public void DecreaseResource(int amount, int tierID)
    {
        if (tierID <= resourcesAmounts.Length && tierID > 0)
        {
            if (resourcesAmounts[tierID - 1] - amount < 0)
            {
                resourcesAmounts[tierID - 1] = 0;
            }
            else
            {
                resourcesAmounts[tierID - 1] -= amount;
            }
            counterTexts[tierID - 1].text = resourcesAmounts[tierID - 1].ToString();
        }
        else
        {
            Debug.Log("Invalid TierID: " + tierID);
        }
    }

    public int[] GetResources()
    {
        return resourcesAmounts;
    }

    public void SetResources(int[] resources)
    {
        resourcesAmounts = resources;
        int i = 0;
        foreach (TMP_Text text in counterTexts)
        {
            text.text = resourcesAmounts[i].ToString();
            i++;
        }
    }
}
