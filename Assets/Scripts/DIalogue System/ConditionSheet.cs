using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    FirstTalkWithMayor,
    FirstItemCrafted,
    TrashMilestoneOne,
    TrashMilestoneTwo
}

[System.Serializable]
public class ConditionStatus
{
    public Conditions condition;
    public bool status;

    public ConditionStatus() { }
}

[CreateAssetMenu(fileName = "ConditionSheet", menuName = "ConditionSheet", order = 0)]
public class ConditionSheet : ScriptableObject
{
    public ConditionStatus[] conditions;
}
