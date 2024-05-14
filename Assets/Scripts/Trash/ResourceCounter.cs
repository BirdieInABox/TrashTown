using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour, IEventListener
{
    public int numOfTiers = 4;
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
            Upgrade upgradeItem = receivedEvent.Data as Upgrade;
            if (upgradeItem is Backpack)
            {
                maxAmount = (upgradeItem as Backpack).size;
            }
        }
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
}
