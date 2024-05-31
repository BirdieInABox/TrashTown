//Author: Effie Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : Interactable, IEventListener
{
    //The different texts of the dialogue, in order of appearance

    public Dialogue[] dialogues;
    private string[] lines;

    //The speed in which the characters of the text appear
    [SerializeField]
    private float textSpeed;

    //the index of the current text passage of the dialogue
    private int lineIndex;
    private int dialogueIndex;
    private bool inDialogue = false;

    // Start is called before the first frame update
    void Awake()
    {
        //Empty the text-field
        EventManager.MainStatic.AddListener(this);
    }

    public void ChangeSpeed(float speed)
    {
        textSpeed = speed;
    }

    public float GetSpeed()
    {
        return textSpeed;
    }

    public override void Interact()
    {
        if (!inDialogue)
        {
            inDialogue = true;
            EventManager.MainStatic.FireEvent(new EventData(EventType.DialogueToggled));
            DialogueStart(dialogues[dialogueIndex].lines);
        }
        else
        {
            NextLine();
        }
    }

    //Start first instance of this dialogue
    public void OnEventReceived(EventData receivedEvent)
    {
        if (receivedEvent.Type == EventType.TextSpeedChanged)
        {
            textSpeed = (float)receivedEvent.Data;
        }
    }

    public void DialogueStart(string[] npcDialogue)
    {
        // player.toggleDialogue();
        gameObject.SetActive(true);
        DialogueLineData.currentLine = string.Empty;
        lines = npcDialogue;
        //Reset index to first text passage
        lineIndex = 0;
        //Write first line
        StartCoroutine(TypeLine());
    }

    //Writes lines one letter at a time, dependent on the textSpeed variable
    IEnumerator TypeLine()
    {
        //For each character in the current text passage
        foreach (char c in lines[lineIndex].ToCharArray())
        {
            //Add character to text-field
            DialogueLineData.currentLine += c;
            //return and wait according to the textSpeed
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //Goes to the next passage-
    public void NextLine()
    {
        //if the entire text passage is currently being shown in the text-field
        if (DialogueLineData.currentLine == lines[lineIndex])
        {
            //If the end of the dialogue hasn't been reached yet
            if (lineIndex < lines.Length - 1)
            {
                //Set index to next passage
                lineIndex++;
                //Empty the text-field
                DialogueLineData.currentLine = string.Empty;
                //Write the next line
                StartCoroutine(TypeLine());
            }
            else //If the end of the dialogue (last passage) has been reached
            {
                lines = null;
                // player.toggleDialogue();
                //Hide the dialogue window
                inDialogue = false;
                EventManager.MainStatic.FireEvent(new EventData(EventType.DialogueToggled));
            }
        }
        else //If the current text passage is currently NOT being fully shown in the text-field
        {
            //Skip the letter-by letter text
            StopAllCoroutines();
            //Show the current text passage instantly
            DialogueLineData.currentLine = lines[lineIndex];
        }
    }
}
