using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMeinenEventmaager : MonoBehaviour , EventListenerTesting
{
    private void Start()
    {
        EventManagerTesting.MainStatic.AddListener(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IchWillDingeSenden();
        }
    }

    public void OnEventReceived(EventDataTesting receivedEvent)
    {
        switch (receivedEvent.Type)
        {
            case EventDataTypeTesting.TesteMich:
                IchWillDingeEmpfangen((string)receivedEvent.Data);
                break;

            case EventDataTypeTesting.TesteMichNochmal:
                LetzterTest((float)receivedEvent.Data);
                break;

            default:
                break;
        }
    }

    public void IchWillDingeSenden()
    {
        Debug.Log("IchWillDingeSenden");
        EventManagerTesting.MainStatic.FireEvent(new EventDataTesting(EventDataTypeTesting.TesteMich, "Erstes Senden erfolgt"));
    }

    public void IchWillDingeEmpfangen(string receiveString)
    {
        Debug.Log("IchWillDingeEmpfangen "+ receiveString);
        EventManagerTesting.MainStatic.FireEvent(new EventDataTesting(EventDataTypeTesting.TesteMichNochmal, 2f));
    }

    public void LetzterTest(float receiveFloat)
    {
        Debug.Log("FERTIG! "+receiveFloat);
    }
}
