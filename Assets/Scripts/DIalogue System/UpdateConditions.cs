//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;

public class UpdateConditions : MonoBehaviour, IEventListener
{
    public ConditionSheet sheet;

    private void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.ConditionChanged)
        {
            UpdateSheet((ConditionStatus)receivedEvent.Data);
        }
    }

    public void UpdateSheet(ConditionStatus newCondition)
    {
        Predicate<ConditionStatus> predicate = FindCondition;

        bool FindCondition(ConditionStatus condition)
        {
            if (condition.status == newCondition.status)
            {
                condition = newCondition;
                return true;
            }
            else
                return false;
        }
        Array.Find<ConditionStatus>(sheet.conditions, predicate);
    }
}
