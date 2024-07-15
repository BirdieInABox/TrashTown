//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//A set of costs
public class Costs
{
    //all costs in a sorted array
    public int[] tierCosts = new int[4];

    public Costs(int one, int two, int three, int four)
    {
        tierCosts[0] = one;
        tierCosts[1] = two;
        tierCosts[2] = three;
        tierCosts[3] = four;
    }
}

public class CraftingOverview : MonoBehaviour, IEventListener
{
    [SerializeField] //the displayed icon of the displayed upgrade
    private Image image;

    [SerializeField] //the displayed name of the upgrade
    private TMP_Text upgradeName;

    [SerializeField] //text fields showing each tier's cost
    private TMP_Text tierOne,
        tierTwo,
        tierThree,
        tierFour;

    [SerializeField] //the button initiating the craft
    private Button craftButton;
    private Upgrade upgrade; //the upgrade to be displayed

    [SerializeField] //a reference to the resource counter
    private ResourceCounter resources;

    [SerializeField] //the player's current upgrades
    private Upgrades upgrades;
    private Costs costs; //a set of costs

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
        //if the received event is of type "CraftingItemSelected"
        if (receivedEvent.Type == EventType.CraftingItemSelected)
        {
            //get a generic upgrade from the payload
            upgrade = (Upgrade)receivedEvent.Data;
            //if the upgrade is of type AirBottle
            if (upgrade is AirBottle)
            {
                //Update the displayed information for an AirBottle
                UpdateInformation(upgrade as AirBottle);
            } //if the upgrade is of type SeaScooter
            else if (upgrade is SeaScooter)
            {
                //Update the displayed information for a Sea Scooter
                UpdateInformation(upgrade as SeaScooter);
            } //if the upgrade is of type Backpack
            else if (upgrade is Backpack)
            {
                //Update the displayed information for a Backpack
                UpdateInformation(upgrade as Backpack);
            }
        }
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Updates all displayed information in the UI
    /// /// and unlocks the craft, if all requirements are fulfilled
    /// </summary>
    /// <param name="upgrade">the upgrade to be displayed</param>
    private void UpdateInformation(AirBottle upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        tierFour.SetText(upgrade.tierFourCost.ToString());
        costs = new Costs(
            upgrade.tierOneCost,
            upgrade.tierTwoCost,
            upgrade.tierThreeCost,
            upgrade.tierFourCost
        );
        //Checks if the player already owns an airBottle, as well as the player's airBottle being of lower tier than the new one
        //Then check the other requirements and potentially unlock the craft
        UnlockCraft((upgrades.airBottle != null) && (upgrade.tier <= upgrades.airBottle.tier));
    }

    /// <summary>
    /// Updates all displayed information in the UI
    /// /// and unlocks the craft, if all requirements are fulfilled
    /// </summary>
    /// <param name="upgrade">the upgrade to be displayed</param>
    private void UpdateInformation(Backpack upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        tierFour.SetText(upgrade.tierFourCost.ToString());
        costs = new Costs(
            upgrade.tierOneCost,
            upgrade.tierTwoCost,
            upgrade.tierThreeCost,
            upgrade.tierFourCost
        );
        //Checks if the player already owns a backpack, as well as the player's backpack being of lower tier than the new one
        //Then check the other requirements and potentially unlock the craft
        UnlockCraft((upgrades.backpack != null) && upgrade.tier <= upgrades.backpack.tier);
    }

    /// <summary>
    /// Updates all displayed information in the UI
    /// and unlocks the craft, if all requirements are fulfilled
    /// </summary>
    /// <param name="upgrade">the upgrade to be displayed</param>
    private void UpdateInformation(SeaScooter upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        tierFour.SetText(upgrade.tierFourCost.ToString());
        costs = new Costs(
            upgrade.tierOneCost,
            upgrade.tierTwoCost,
            upgrade.tierThreeCost,
            upgrade.tierFourCost
        );
        //Checks if the player already owns a seaScooter, as well as the player's seaScooter being of lower tier than the new one
        //Then check the other requirements and potentially unlock the craft
        UnlockCraft((upgrades.scooter != null) && upgrade.tier <= upgrades.scooter.tier);
    }

    /// <summary>
    /// Checks the craft's requirements and potentially unlocks (enables) the crafting button
    /// </summary>
    /// <param name="tierLower">the bool if the player has an upgrade of this type and if it's of higher tier</param>
    private void UnlockCraft(bool tierLower)
    {
        //Get a snapshot of the player's current resources
        int[] resourceAmounts = resources.GetResources();
        //a bool if the button should stay locked
        bool stayLocked = tierLower;

        //if no requirements have been hurt so far
        if (!stayLocked)
        {
            //iterate through the resources and check if any of them are below the needed amount for crafting the item
            for (int i = 0; i < resourceAmounts.Length; )
            {
                stayLocked = stayLocked || (resourceAmounts[i] < costs.tierCosts[i]);
                i++;
            }
        }
        //(un)lock craft button
        craftButton.interactable = !stayLocked;
    }

    //locks the button
    private void LockCraft()
    {
        craftButton.interactable = false;
    }

    public void ToggleCrafting()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingToggled));
    }

    /// <summary>
    /// Called by the craft button, which can only be enabled if requirements have been fulfilled. Locks the craft and sends out events
    /// </summary>
    public void OnCraft()
    {
        //lock craft
        LockCraft();
        //send an event of type "ItemCrafted" to the event system with the new upgrade as payload
        EventManager.MainStatic.FireEvent(new EventData(EventType.ItemCrafted, upgrade));

        //Send an event of type "ConditionChanged" to the event system with the now changed condition as payload
        EventManager.MainStatic.FireEvent(
            new EventData(
                EventType.ConditionChanged,
                new ConditionStatus(Conditions.FirstItemCrafted, true)
            )
        );
    }
}
