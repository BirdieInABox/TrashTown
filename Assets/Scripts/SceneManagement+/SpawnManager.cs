using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;

    private void Start()
    {
        EventManager.MainStatic.FireEvent(new EventData(EventType.SetSpawn, spawnPoint));
    }
}
