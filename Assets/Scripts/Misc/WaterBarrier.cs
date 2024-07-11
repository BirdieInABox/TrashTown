//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBarrier : MonoBehaviour
{
    [SerializeField] //the minimum bottle needed to pass this barrier
    private AirBottle neededBottle;

    /// <summary>
    /// called when player enters this barrier's trigger
    /// destroys the barrier, if the player's bottle is sufficient
    /// </summary>
    /// <param name="currentBottle"> the player's current air bottle</param>
    public void CheckUpgrade(AirBottle currentBottle)
    {
        //if the bottle is at least of the needed tier, or higher
        if (currentBottle.tier >= neededBottle.tier)
        {
            //Destroy this barrier
            Destroy(gameObject);
        }
    }
}
