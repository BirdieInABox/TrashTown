//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All different possible conditions
public enum Conditions
{
    FirstTalkWithMayor,
    FirstItemCrafted,
    TrashMilestoneOne,
    TrashMilestoneTwo
}

// a tuple of condition and the condition's status
[System.Serializable]
public class ConditionStatus
{
    public Conditions condition;
    public bool status;

    public ConditionStatus(Conditions _condition, bool _status)
    {
        status = _status;
        condition = _condition;
    }
}

//Only one condition sheet should exist.
//It is used to save the player's currently fulfilled and unfulfilled conditions
[CreateAssetMenu(fileName = "ConditionSheet", menuName = "ConditionSheet", order = 0)]
[System.Serializable]
public class ConditionSheet : ScriptableObject
{
    public ConditionStatus[] conditions;
}
