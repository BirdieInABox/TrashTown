//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounter : MonoBehaviour, IEventListener
{
    public int numOfTiers = 4; //the amount of different trash tiers
    private int[] resourcesAmounts; //each internal counter
    private TMP_Text[] counterTexts; //each UI counter

    [SerializeField] //the player's upgrades
    private Upgrades upgrades;
    private int maxAmount; //the maximum amount of resources collected of each tier

    void Awake()
    {
        //the amount of resources allowed by the current backpack
        maxAmount = upgrades.GetSize();
        //initialize amounts and UI
        resourcesAmounts = new int[numOfTiers];
        counterTexts = new TMP_Text[numOfTiers];
        //iterate through children to not have to get the component every time the UI is changed
        int i = 0;
        foreach (Transform child in transform)
        {
            //get UI text components
            if (i < counterTexts.Length)
                counterTexts[i] = child.GetComponentInChildren<TMP_Text>();
            i++;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //Add this as listener to the event system
        EventManager.MainStatic.AddListener(this);
    }

    /// <summary>
    /// Called upon EventSystem sending an event
    /// </summary>
    /// <param name="receivedEvent">the received event, including type and content</param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //if event received is of type TrashCollected
        if (receivedEvent.Type == EventType.TrashCollected)
        {
            //Increase counter using the event's payload
            IncreaseResource((TrashObject)receivedEvent.Data);
        }
        //if the event received is of type "ItemCrafted"
        else if (receivedEvent.Type == EventType.ItemCrafted)
        {
            //get payload as generic Upgrade
            Upgrade upgrade = receivedEvent.Data as Upgrade;
            //if upgrade is of type AirBottle
            if (upgrade is AirBottle)
            {
                //spend resources needed for craft
                DecreaseResourceOnCraft(upgrade as AirBottle);
            }
            //if upgrade is of type SeaScooter
            else if (upgrade is SeaScooter)
            {
                //spend resources needed for craft
                DecreaseResourceOnCraft(upgrade as SeaScooter);
            }
            //if upgrade is of type Backpack
            else if (upgrade is Backpack)
            {
                //spend resources needed for craft
                DecreaseResourceOnCraft(upgrade as Backpack);
                //Update max amount on backpack crafted
                maxAmount = (upgrade as Backpack).size;
            }
        }
    }

    /// <summary>
    /// Decreases each resource for the item's costs
    /// </summary>
    /// <param name="airbottle">the upgrade crafted</param>
    private void DecreaseResourceOnCraft(AirBottle airbottle)
    {
        DecreaseResource(airbottle.tierOneCost, 1);
        DecreaseResource(airbottle.tierTwoCost, 2);
        DecreaseResource(airbottle.tierThreeCost, 3);
        DecreaseResource(airbottle.tierFourCost, 4);
    }

    /// <summary>
    /// Decreases each resource for the item's costs
    /// </summary>
    /// <param name="scooter">the upgrade crafted</param>
    private void DecreaseResourceOnCraft(SeaScooter scooter)
    {
        DecreaseResource(scooter.tierOneCost, 1);
        DecreaseResource(scooter.tierTwoCost, 2);
        DecreaseResource(scooter.tierThreeCost, 3);
        DecreaseResource(scooter.tierFourCost, 4);
    }

    /// <summary>
    /// Decreases each resource for the item's costs
    /// </summary>
    /// <param name="backpack">the upgrade crafted</param>
    private void DecreaseResourceOnCraft(Backpack backpack)
    {
        DecreaseResource(backpack.tierOneCost, 1);
        DecreaseResource(backpack.tierTwoCost, 2);
        DecreaseResource(backpack.tierThreeCost, 3);
        DecreaseResource(backpack.tierFourCost, 4);
    }

    /// <summary>
    /// Increases a resource by a set amount for a set tier, depending on the object
    /// </summary>
    /// <param name="obj">the object with the set tier and value</param>
    public void IncreaseResource(TrashObject obj)
    {
        //Get the worth of the object
        int amount = obj.trash.trashWorth;
        //Get the tier of the objet
        int tierID = obj.trash.trashTier;
        //if it's a legal tier and the tier isn't 0
        if (tierID <= resourcesAmounts.Length && tierID > 0)
        {
            //(for easier use by designers, the tiers have indices 1-4 instead of 0-3,
            //which then has to be corrected here)

            //If the current amount with the extra amount added is more than the backpack can hold
            if (resourcesAmounts[tierID - 1] + amount > maxAmount)
            {
                //Set the currenta mount to the maximum amount
                resourcesAmounts[tierID - 1] = maxAmount;
            }
            else //if the maximum amount will not be reached
            {
                //increase the current amount by the worth of the object
                resourcesAmounts[tierID - 1] += amount;
            }
            //update the UI
            counterTexts[tierID - 1].text =
                resourcesAmounts[tierID - 1].ToString() + " / " + maxAmount.ToString();
        }
        else //if tier is illegal
        {
            Debug.Log("Invalid TierID: " + tierID);
        }
    }

    /// <summary>
    /// Decreases the amount of one tier by the amount given
    /// </summary>
    /// <param name="amount">amount to be subtracted</param>
    /// <param name="tierID">the tier to be reduced</param>
    public void DecreaseResource(int amount, int tierID)
    {
        //if the tier is legal
        if (tierID <= resourcesAmounts.Length && tierID > 0)
        {
            //(for easier use by designers, the tiers have indices 1-4 instead of 0-3,
            //which then has to be corrected here)

            //if the current amount minus the number to be subtracted are less than 0
            if (resourcesAmounts[tierID - 1] - amount < 0)
            {
                //Set counter to 0, do not allow negative numbers
                resourcesAmounts[tierID - 1] = 0;
            }
            else //if negative numbers are not reached
            {
                //decrease amount
                resourcesAmounts[tierID - 1] -= amount;
            }
            counterTexts[tierID - 1].text = resourcesAmounts[tierID - 1].ToString();
        }
        else //if the tier is illegal
        {
            Debug.Log("Invalid TierID: " + tierID);
        }
    }

    /// <returns>an array of all resources</returns>
    public int[] GetResources()
    {
        return resourcesAmounts;
    }

    /// <summary>
    /// Sets all resource amounts to the amounts in the parameter
    /// </summary>
    /// <param name="resources">the new resource amounts</param>
    public void SetResources(int[] resources)
    {
        //copy the array to the resourceAmounts
        resourcesAmounts = resources;

        //iterate through the UI and update it
        int i = 0;
        foreach (TMP_Text text in counterTexts)
        {
            text.text = resourcesAmounts[i].ToString() + " / " + maxAmount.ToString();
            i++;
        }
    }
}
