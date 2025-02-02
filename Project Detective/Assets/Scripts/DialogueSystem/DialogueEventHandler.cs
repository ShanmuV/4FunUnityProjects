using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEventHandler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerManager player;

    [SerializeField] private GameObject dialoguePanel;

    [Header("Belongs to the Chosen One")]
    [SerializeField] private InteractableNPC chosenOneNPC;
    [SerializeField] private GameObject ChosenOnePanel;
    [SerializeField] private Dialogue chosenOneDialogue;
    [SerializeField] private Dialogue afterChosenOneDialogue;


    public void StartEvent(DialogueEvents dialogueEvent)
    {
        switch (dialogueEvent)
        {
            case DialogueEvents.BelongsToTheOne:
                FireBelongsToTheChosenOneEvent();
                break;

            default:
                Debug.LogError("No Switch-case statement in Dialogue Event Handler");
                break;
        }
    }
    
    public void StopEvent(DialogueEvents dialogueEvent)
    {
        switch (dialogueEvent)
        {
            case DialogueEvents.BelongsToTheOne:
                StopBelongsToTheChosenOneEvent();
                break;

            default:
                Debug.LogError("No Switch-case statement in Dialogue Event Handler");
                break;
        }
    }

    private void FireBelongsToTheChosenOneEvent()
    {
        ChosenOnePanel.SetActive(true);
        dialoguePanel.SetActive(false);
        chosenOneNPC.ChangeDialogue(chosenOneDialogue);
        player.ChangeState(PlayerManager.PlayerState.inEvent);
    }

    private void StopBelongsToTheChosenOneEvent()
    {
        ChosenOnePanel.SetActive(false);
        player.ChangeState(PlayerManager.PlayerState.Normal);
        chosenOneNPC.ChangeDialogue(afterChosenOneDialogue);
    }
}
