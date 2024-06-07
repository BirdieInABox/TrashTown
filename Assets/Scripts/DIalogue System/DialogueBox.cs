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
        EventManager.MainStatic.AddListener(this);
        dialogueUI = gameObject.transform.GetChild(0).gameObject;
        dialogueText = dialogueUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.DialogueToggled)
        {
            dialogueUI.SetActive(!dialogueUI.activeSelf);
        }
    }

    private void Update()
    {
        dialogueText.SetText(DialogueLineData.currentLine);
    }
}
