using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBarrier : MonoBehaviour
{
    [SerializeField]
    private AirBottle neededBottle;

    public void CheckUpgrade(AirBottle currentBottle)
    {
        if (currentBottle.tier >= neededBottle.tier)
        {
            Destroy(gameObject);
        }
    }
}
