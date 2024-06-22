//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour, IEventListener
{
    public List<TrashObject> trashObjects = new List<TrashObject>();

    [SerializeField]
    private ResourceCounter counter;

    void Awake()
    {
        foreach (Transform child in gameObject.transform)
        {
            trashObjects.Add(child.GetComponent<TrashObject>());
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
        {
            RemoveTrash((TrashObject)receivedEvent.Data);
        }
    }

    public void RemoveTrash(TrashObject obj)
    {
        // counter.IncreaseResource(obj.trash.trashWorth, obj.trash.trashTier);
        trashObjects.Remove(obj);
    }

    public void UpdateTrash(List<TrashObject> newTrashObjects)
    {
        for (int i = trashObjects.Count - 1; i >= 0; i--)
        {
            TrashObject obj = trashObjects[i];
            if (!newTrashObjects.Contains(obj))
            {
                Debug.Log("Destroy Trash");
                Destroy(obj.gameObject);
                RemoveTrash(obj);
            }
        }
    }
}
