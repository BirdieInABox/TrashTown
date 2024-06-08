using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using MessagePack;
using System;

public class SavingManager : MonoBehaviour, IEventListener
{
    [SerializeField]
    private TrashCollection trash;

    [SerializeField]
    private Upgrades upgrades;

    [SerializeField]
    private ResourceCounter resources;

    [SerializeField]
    private ConditionSheet conditions;

    void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void SaveGameState(SavingData saveData)
    {
        byte[] bytes = MessagePackSerializer.Serialize(saveData);
        FileStream file = new FileStream(
            Application.persistentDataPath + "/SaveData.save",
            FileMode.Create
        );
        //file = File.Create(Application.persistentDataPath + "/SaveData.save");
        file.Close();
        var json = MessagePackSerializer.ConvertToJson(bytes);
        Console.WriteLine(json);
    }

    public void LoadGameState() { }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.SaveGame)
        {
            SavingData data = new SavingData();
            data.conditions = conditions;
            data.resourceAmounts = resources.GetResources();
            data.airBottle = upgrades.airBottle;
            data.scooter = upgrades.scooter;
            data.backpack = upgrades.backpack;
            data.trashObjects = trash.trashObjects;

            SaveGameState(data);
        }
    }
}

[MessagePackObject(keyAsPropertyName: true)]
public class SavingData
{
    public ConditionSheet conditions;

    public int[] resourceAmounts;

    public AirBottle airBottle;

    public SeaScooter scooter;

    public Backpack backpack;

    public List<TrashObject> trashObjects;
}
