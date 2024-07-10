//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBox : MonoBehaviour, IEventListener
{
    private GameObject dialogueUI;
    private TextMeshProUGUI dialogueText;

    private void Start()
    {
        //Add this as listener to the event system
        EventManager.MainStatic.AddListener(this);
        //Get both components needed
        dialogueUI = gameObject.transform.GetChild(0).gameObject;
        dialogueText = dialogueUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Gets called when receiving an event sent by the event system
    /// </summary>
    /// <param name="receivedEvent"> the received event </param>
    public void OnEventReceived(EventData receivedEvent)
    {
        //If the received event is of type "DialogueToggled"
        if (receivedEvent.Type == EventType.DialogueToggled)
        {
            //Toggle the dialogue UI
            dialogueUI.SetActive(!dialogueUI.activeSelf);
        }
    }

    private void Update()
    {
        //Keep the UI's text the same as the current line in the DialogueLineData
        dialogueText.SetText(DialogueLineData.currentLine);
    }
}
