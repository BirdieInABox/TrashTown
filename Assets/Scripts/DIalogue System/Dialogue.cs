//Author: Kim Effie Proestler
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The dialogue sheet
//Each dialogue needs one
[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue", order = 0)]
public class Dialogue : ScriptableObject
{
    //The conditions under which the dialogue is triggered
    public ConditionStatus[] conditions;

    //The lines of the dialogue.
    [TextArea]
    public string[] lines;
}
