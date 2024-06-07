//Author: Tim Böttcher
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventListener
{
    void OnEventReceived(EventData data);
}
