using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAnimations
{
    public void ChoiceButtonSlide(GameObject button)
    {
        Vector2 position = button.transform.position;
        button.LeanAlpha(0f, 0f);
        button.transform.position = new Vector2(position.x - 2f, position.y);
        button.transform.LeanMove(position, 0.25f);
        button.LeanAlpha(1f, 0.1f);
    }
}
