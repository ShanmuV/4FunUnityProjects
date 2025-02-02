using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is for the Objects / Decorations / props in the area, that contains Dialogue */

public class Object : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;

    public Dialogue GetDialogue()
    {
        return dialogue;
    }
}
