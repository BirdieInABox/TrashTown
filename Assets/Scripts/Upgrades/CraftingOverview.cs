//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Costs
{
    public int[] tierCosts = new int[3];

    public Costs(int one, int two, int three)
    {
        Debug.Log(one + ", " + two + ", " + three);
        tierCosts[0] = one;
        tierCosts[1] = two;
        tierCosts[2] = three;
    }
}

public class CraftingOverview : MonoBehaviour, IEventListener
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text upgradeName;

    [SerializeField]
    private TMP_Text tierOne;

    [SerializeField]
    private TMP_Text tierTwo;

    [SerializeField]
    private TMP_Text tierThree;

    [SerializeField]
    private Button craftButton;
    private Upgrade upgrade;

    [SerializeField]
    private ResourceCounter resources;

    [SerializeField]
    private Upgrades upgrades;
    private Costs costs;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.CraftingItemSelected)
        {
            upgrade = (Upgrade)receivedEvent.Data;
            if (upgrade is AirBottle)
            {
                UpdateInformation(upgrade as AirBottle);
            }
            else if (upgrade is SeaScooter)
            {
                UpdateInformation(upgrade as SeaScooter);
            }
            else if (upgrade is Backpack)
            {
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

    private void UpdateInformation(AirBottle upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        costs = new Costs(upgrade.tierOneCost, upgrade.tierTwoCost, upgrade.tierThreeCost);

        UnlockCraft((upgrades.airBottle != null) && (upgrade.tier <= upgrades.airBottle.tier));
    }

    private void UpdateInformation(Backpack upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        costs = new Costs(upgrade.tierOneCost, upgrade.tierTwoCost, upgrade.tierThreeCost);
        UnlockCraft((upgrades.backpack != null) && upgrade.tier <= upgrades.backpack.tier);
    }

    private void UpdateInformation(SeaScooter upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
        costs = new Costs(upgrade.tierOneCost, upgrade.tierTwoCost, upgrade.tierThreeCost);
        UnlockCraft((upgrades.scooter != null) && upgrade.tier <= upgrades.scooter.tier);
    }

    private void UnlockCraft(bool tierLower)
    {
        int[] resourceAmounts = resources.GetResources();
        bool stayLocked = tierLower;
        Debug.Log(resourceAmounts.Length);

        if (!stayLocked)
        {
            for (int i = 0; i < resourceAmounts.Length; )
            {
                stayLocked = stayLocked || (resourceAmounts[i] < costs.tierCosts[i]);
                i++;
            }
        }
        craftButton.interactable = !stayLocked;
    }

    private void LockCraft()
    {
        craftButton.interactable = false;
    }

    public void ToggleCrafting()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.CraftingToggled));
    }

    public void OnCraft()
    {
        LockCraft();
        EventManager.MainStatic.FireEvent(new EventData(EventType.ItemCrafted, upgrade));

        EventManager.MainStatic.FireEvent(
            new EventData(
                EventType.ConditionChanged,
                new ConditionStatus(Conditions.FirstItemCrafted, true)
            )
        );
    }
}
