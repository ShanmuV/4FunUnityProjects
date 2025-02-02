using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    // inspector Stuff
    public bool hasLines, hasChoices, hasEventStopper;

    public DialogueEvents dialogueEvent;

    public enum Type
    {
        Normal,
        Choices,
        SceneChange,
        FireEvent
    }
    public Type type;

    public string[] lines;

    public List<Choice> choices = new List<Choice>();

    public string sceneName;

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName);
    }

    public string[] GetLines()
    {
        return lines;
    }

    public DialogueEvents GetDialogueEvent()
    {
        return dialogueEvent;
    }


}
