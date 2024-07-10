//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;

public class UpdateConditions : MonoBehaviour, IEventListener
{
    //The universal condition sheet
    public ConditionSheet sheet;

    //Add this as listener to the event system
    private void Start()
    {
        EventManager.MainStatic.AddListener(this);
    }

    /// <summary>
    /// Called upon receiving an event from the event system
    /// </summary>
    /// <param name="receivedEvent"> the received event </param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //If the received event is of type "ConditionChanged
        if (receivedEvent.Type == EventType.ConditionChanged)
        {
            //Update the sheet with the condition and status in the event data's payload
            UpdateSheet((ConditionStatus)receivedEvent.Data);
        }
    }

    /// <summary>
    /// Finds the condition contained in the event's payload within the universal condition sheet and updates its status
    /// to the status in the event's payload.
    /// </summary>
    /// <param name="newCondition"> the event's payload, consisting of a condition and a status </param>
    public void UpdateSheet(ConditionStatus newCondition)
    {
        //Finds the condition in the condition sheet
        foreach (ConditionStatus conditionStatus in sheet.conditions)
        {
            //when the condition has been found
            if (conditionStatus.condition == newCondition.condition)
            {
                //updates it status and leave
                conditionStatus.status = newCondition.status;
                break;
            }
        }
    }
}
