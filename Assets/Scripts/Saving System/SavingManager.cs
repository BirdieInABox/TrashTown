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
    //All origins for data that has to be saved
    [SerializeField] //all remaining trash objects
    private TrashCollection landTrash,
        waterTrash;
    private List<TrashObject> landTrashObjects,
        waterTrashObjects;

    [SerializeField] //all crafted upgrades
    private Upgrades upgrades;

    [SerializeField] //All currently available resources
    private ResourceCounter resources;

    [SerializeField] //The sheet of universal conditions
    private ConditionSheet conditions;

    //the encryption key and initialization vector
    private byte[] encKey,
        encIV;

    //The path at which the save file is saved to within the game's PersistentDataPath
    private string savePath = "/SaveData.save";

    void Awake()
    {
        //Change the IV and Key to hardcoded versions

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //!!!! CENSOR THOSE BEFORE PUBLISHING THE CODE !!!!
        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        encIV = Encoding.ASCII.GetBytes("1111111111111111");
        encKey = Encoding.ASCII.GetBytes("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
    }

    private void Start()
    {
        //Add this as listener to the Event System
        EventManager.MainStatic.AddListener(this);
        LoadGameState();
    }

    /// <summary>
    /// Saves and encrypts the current game state to a file, using a set of data
    /// </summary>
    /// <param name="saveData">the set of data to be saved</param>
    public void SaveGameState(SavingData saveData)
    {
        //Transform the data to be saved into a readable string
        string saveString = JsonUtility.ToJson(saveData);
        //encrypt the string to an array of bytes
        byte[] bytes = EncryptionManager.EncryptToBytes(saveString, encKey, encIV);
        //Write the encrypted bytes to a file
        FileStream file = new FileStream(
            Application.persistentDataPath + savePath,
            FileMode.Create
        );
        file.Write(bytes);
        //close the stream
        file.Close();
    }

    /// <summary>
    /// Loads the game and puts all data where it belongs
    /// </summary>
    public void LoadGameState()
    {
        //If a save file exists
        if (File.Exists(Application.persistentDataPath + savePath))
        {
            //read the encrypted array of bytes from a set path
            byte[] encryptedData = File.ReadAllBytes(Application.persistentDataPath + savePath);
            //decrypt the bytes to a readable string
            string loadString = EncryptionManager.DecryptFromBytes(encryptedData, encKey, encIV);
            //transform the string to a set of data
            SavingData data = JsonUtility.FromJson<SavingData>(loadString);
            //update the condition sheet with the saved conditions
            conditions = data.conditions;
            //update the resources with the saved ones
            resources.SetResources(data.resourceAmounts);
            //update the player's upgrades
            upgrades.airBottle = data.airBottle;
            upgrades.scooter = data.scooter;
            upgrades.backpack = data.backpack;
            //Temp-save already collected trash for in-between scenes
            landTrashObjects = data.landTrashObjects;
            waterTrashObjects = data.waterTrashObjects;
            //remove already collected trash
            if (landTrash != null)
                landTrash.UpdateTrash(landTrashObjects);
            if (waterTrash != null)
                waterTrash.UpdateTrash(waterTrashObjects);
        }
    }

    /// <summary>
    /// Called upon EventSystem sending an event
    /// </summary>
    /// <param name="receivedEvent">the received event, including type and content</param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //if the received event is of type "SaveGame"
        if (receivedEvent.Type == EventType.SaveGame)
        {
            //save the game
            SaveGameState(createData());
        }
        else if (receivedEvent.Type == EventType.LoadGame)
        {
            //Load the game
            LoadGameState();
        }
    }

    /// <summary>
    /// Create a set of data using the referenced objects
    /// </summary>
    /// <returns>the set of data to be saved</returns>
    private SavingData createData()
    {
        //Create new SavingData
        SavingData data = new SavingData();
        //Get universal conditions
        data.conditions = conditions;
        //Get current amounts of trash
        data.resourceAmounts = resources.GetResources();
        //Get current upgrades
        data.airBottle = upgrades.airBottle;
        data.scooter = upgrades.scooter;
        data.backpack = upgrades.backpack;

        //Get remaining trash objects
        if (landTrash != null) //if there is an updated list
            landTrashObjects = landTrash.trashObjects; //update list
        data.landTrashObjects = landTrashObjects; //else take old list
        if (waterTrash != null) //if there is an updated list
            waterTrashObjects = waterTrash.trashObjects; //update list
        data.waterTrashObjects = waterTrashObjects; //else take old list

        return data;
    }
}

/// <summary>
/// A set of data, used as complex type for saving all data at once
/// </summary>
public class SavingData
{
    //universal conditions
    public ConditionSheet conditions;

    //amounts of trash collected
    public int[] resourceAmounts;

    //player upgrades
    public AirBottle airBottle;

    public SeaScooter scooter;

    public Backpack backpack;

    //list of remaining trash objects
    public List<TrashObject> waterTrashObjects,
        landTrashObjects;
}
