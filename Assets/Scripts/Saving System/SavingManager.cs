//Author: Kim Effie Pröstler
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

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
    private byte[] encKey,
        encIV;

    void Awake()
    {
        EventManager.MainStatic.AddListener(this);
        encIV = Encoding.ASCII.GetBytes("8472634872343934");
        encKey = Encoding.ASCII.GetBytes("kajsedhasjkdahsdkahsdkjaasdjkhas");
        LoadGameState();
    }

    public void SaveGameState(SavingData saveData)
    {
        string saveString = JsonUtility.ToJson(saveData);
        byte[] bytes = EncryptionManager.EncryptToBytes(saveString, encKey, encIV);
        FileStream file = new FileStream(
            Application.persistentDataPath + "/SaveData.save",
            FileMode.Create
        );
        file.Write(bytes);
        file.Close();
    }

    public void LoadGameState()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.save"))
        {
            byte[] encryptedData = File.ReadAllBytes(
                Application.persistentDataPath + "/SaveData.save"
            );
            string loadString = EncryptionManager.DecryptFromBytes(encryptedData, encKey, encIV);

            Debug.Log(loadString);
            SavingData data = JsonUtility.FromJson<SavingData>(loadString);
            conditions = data.conditions;
            resources.SetResources(data.resourceAmounts);
            upgrades.airBottle = data.airBottle;
            upgrades.scooter = data.scooter;
            upgrades.backpack = data.backpack;
            trash.UpdateTrash(data.trashObjects);
        }
    }

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

public class SavingData
{
    public ConditionSheet conditions;

    public int[] resourceAmounts;

    public AirBottle airBottle;

    public SeaScooter scooter;

    public Backpack backpack;

    public List<TrashObject> trashObjects;
}
