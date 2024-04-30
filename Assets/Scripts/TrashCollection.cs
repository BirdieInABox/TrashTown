using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollection : MonoBehaviour
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

    public void RemoveTrash(TrashObject obj)
    {
        counter.IncreaseResource(obj.trash.trashWorth, obj.trash.trashTier);
        trashObjects.Remove(obj);
    }
}
