//Author: Effie Bolender
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : Interactable, IEventListener
{
    //The text-field of the dialogue window
    [SerializeField]
    private TextMeshProUGUI textContent;

    [SerializeField]
    private PlayerController player;

    //The different texts of the dialogue, in order of appearance

    public Dialogue[] dialogues;
    private string[] lines;

    //The speed in which the characters of the text appear
    [SerializeField]
    private float textSpeed;

    //the index of the current text passage of the dialogue
    private int index;

    // Start is called before the first frame update
    void Awake()
    {
        //Empty the text-field
        //    textContent.text = string.Empty;
    }

    public void ChangeSpeed(float speed)
    {
        textSpeed = speed;
    }

    public float GetSpeed()
    {
        return textSpeed;
    }

    public override void Interact() { }

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
        textContent.text = string.Empty;
        lines = npcDialogue;
        //Reset index to first text passage
        index = 0;
        //Write first line
        StartCoroutine(TypeLine());
    }

    //Writes lines one letter at a time, dependent on the textSpeed variable
    IEnumerator TypeLine()
    {
        //For each character in the current text passage
        foreach (char c in lines[index].ToCharArray())
        {
            //Add character to text-field
            textContent.text += c;
            //return and wait according to the textSpeed
            yield return new WaitForSeconds(textSpeed);
        }
    }

    //Goes to the next passage-
    public void NextLine()
    {
        //if the entire text passage is currently being shown in the text-field
        if (textContent.text == lines[index])
        {
            //If the end of the dialogue hasn't been reached yet
            if (index < lines.Length - 1)
            {
                //Set index to next passage
                index++;
                //Empty the text-field
                textContent.text = string.Empty;
                //Write the next line
                StartCoroutine(TypeLine());
            }
            else //If the end of the dialogue (last passage) has been reached
            {
                lines = null;
                // player.toggleDialogue();
                //Hide the dialogue window
                gameObject.SetActive(false);
            }
        }
        else //If the current text passage is currently NOT being fully shown in the text-field
        {
            //Skip the letter-by letter text
            StopAllCoroutines();
            //Show the current text passage instantly
            textContent.text = lines[index];
        }
    }

    public void EndDialogue()
    {
        textContent.SetText(string.Empty);
        gameObject.SetActive(false);
    }
}
