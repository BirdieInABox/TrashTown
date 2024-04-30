using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour
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

    public void IncreaseResource(int amount, int tierID)
    {
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
}
