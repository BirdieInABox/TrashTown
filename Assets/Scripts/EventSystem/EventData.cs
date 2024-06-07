//Author: Tim Böttcher, Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventData
{
    public EventType Type { get; private set; }
    public object Data { get; private set; }

    public EventData(EventType type)
        : this(type, null)
    {
        Type = type;
    }

    public EventData(EventType type, object data)
    {
        Type = type;
        Data = data;
    }
}
