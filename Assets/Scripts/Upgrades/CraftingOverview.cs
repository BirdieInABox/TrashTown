using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private Upgrade upgrade;

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
    }

    private void UpdateInformation(Backpack upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
    }

    private void UpdateInformation(SeaScooter upgrade)
    {
        image.sprite = upgrade.icon;
        upgradeName.SetText(upgrade.upgradeName);
        tierOne.SetText(upgrade.tierOneCost.ToString());
        tierTwo.SetText(upgrade.tierTwoCost.ToString());
        tierThree.SetText(upgrade.tierThreeCost.ToString());
    }

    public void OnCraft()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.ItemCrafted, upgrade));
    }
}
