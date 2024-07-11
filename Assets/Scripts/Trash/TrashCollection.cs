//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour, IEventListener
{
    //A list of all remaining trash objects in the scene
    public List<TrashObject> trashObjects = new List<TrashObject>();

    [SerializeField] //a reference to the resource counter
    private ResourceCounter counter;

    void Awake()
    {
        //Iterate through all children and add them to the list of trashObjects
        foreach (Transform child in gameObject.transform)
        {
            trashObjects.Add(child.GetComponent<TrashObject>());
        }
    }

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
        //if the received event is of type "TrashCollected"
        if (receivedEvent.Type == EventType.TrashCollected)
        {
            //Remove the object from the list of remaining trash
            RemoveTrash((TrashObject)receivedEvent.Data);
        }
    }

    /// <summary>
    /// Removes the parameter from the list of remaining trash objects
    /// </summary>
    /// <param name="obj">the object to be removed</param>
    public void RemoveTrash(TrashObject obj)
    {
        //remove object from list
        trashObjects.Remove(obj);
    }

    /// <summary>
    /// Compares the list of remaining trash objects with the list given in the parameter
    /// and deletes objects from the list of remaining objects that are missing in the newTrashObjects list
    /// as well as destroying the associated missing objects
    /// </summary>
    /// <param name="newTrashObjects">the new list of trash objects</param>
    public void UpdateTrash(List<TrashObject> newTrashObjects)
    {
        //iterate through the list of remaining objects
        //(back to front, because we assume we are removing items)
        for (int i = trashObjects.Count - 1; i >= 0; i--)
        {
            //temporarily save the object at the current position
            TrashObject obj = trashObjects[i];
            //if the newTrashObjects list does not contain the object
            if (!newTrashObjects.Contains(obj))
            {
                //Destroy the object in the scene
                Destroy(obj.gameObject);
                //Remove it from the list of remaining objects
                RemoveTrash(obj);
            }
        }
    }
}
