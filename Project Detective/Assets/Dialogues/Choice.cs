using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice 
{
    public string choiceName;
    public Dialogue nextDialogue;

    public Dialogue GetNextDialogue()
    {
        return nextDialogue;
    }

    public override string ToString()
    {
        return choiceName;
    }
}
