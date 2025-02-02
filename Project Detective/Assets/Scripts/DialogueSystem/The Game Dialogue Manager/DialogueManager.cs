using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Linq.Expressions;

public class DialogueManager : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private PlayerManager player;

    [Header("Dialogue Box")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Image textBox;
    [SerializeField] private TextMeshProUGUI TMPText;
    [SerializeField] private DialogueEventHandler dialogueEvents;

    [Header("Choices")]
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private GameObject[] buttons = new GameObject[3];

    [Header("Animations")]
    [SerializeField] private DialogueAnimations animations;



    public InteractableNPC nearest = null;

    public Dialogue currentDialogue = null;

    Queue<string> lines = new Queue<string>();
    List<Choice> choices = new List<Choice>();

    public bool isInDialogue;
    private bool isTyping;



    private void Start()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        InitializeChoiceButtons();
    }

    private void Update()
    {
        if (isInRange())
        {
            nearest = GetNearestNPC();
        }
        else
        {
            nearest = null;
        }





        /* ****  ----- Dialogue Code Below. Put all the other Code above this ------ **** */

        if(nearest == null) return;
        if(Input.GetKeyDown(KeyCode.Return) && nearest.canInteract)
        {
            if(!isInDialogue)
            {
                // Set the InDialogue State to player (Player State)
                PlayDialogue(nearest.dialogue);
            }
            else if(isInDialogue && !isTyping)
            {
                StartCoroutine(PlayNextLine());
            }
            else if(isInDialogue && isTyping)
            {
                FinishTyping();
            }
        }
    }

    private bool isInRange()
    {
        if(player.interactableNPCs.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private InteractableNPC GetNearestNPC()
    {
        float min = 9999f;
        InteractableNPC nearest = null;
        foreach (InteractableNPC npc in player.interactableNPCs)
        {
            if (npc == null) continue;
            if(npc.distanceFromPlayer < min)
            {
                min = npc.distanceFromPlayer;
                nearest = npc;
            }
        }
        SetNearestStatus(nearest);
        return nearest;
    }

    private void SetNearestStatus(InteractableNPC newNearest)
    {
        if (nearest == newNearest) return;
        nearest?.CantInteractWithPlayer();
        newNearest?.CanInteractWithPlayer();
    }

    private void PlayDialogue(Dialogue dialogue)
    {
        isInDialogue = true;
        dialoguePanel.SetActive(true);
        currentDialogue = dialogue;
        StartCoroutine(StartDialogue(currentDialogue));
    }

    private IEnumerator StartDialogue(Dialogue dialogue)
    {
        
        TMPText.text = "";

        // ------------------------------------------------- Dialogue Box Animation and shit

        lines.Clear();
        choices.Clear();
        InitializeChoiceButtons();

        if (dialogue.type == Dialogue.Type.FireEvent)
        {
            dialogueEvents.StartEvent(currentDialogue.GetDialogueEvent());
            EndDialogue();
        }
        else if (currentDialogue.type == Dialogue.Type.SceneChange)
        {
            currentDialogue.SceneChange();
        }
        else
        {
            LoadLines(dialogue);

            LoadChoices(dialogue);

            yield return new WaitForSeconds(0.2f);

            StartCoroutine(PlayNextLine());
        }
        


    }

    private IEnumerator PlayNextLine()
    {

        if (lines.Count == 0)
        {
            if ( currentDialogue.type == Dialogue.Type.Normal)
            EndDialogue();
            if(currentDialogue.type == Dialogue.Type.Choices)
            { }
        }

        else
        {
            TMPText.text = "";
            isTyping = true;
            foreach(var ch in lines.Peek().ToCharArray())
            {
                TMPText.text += ch;
                yield return new WaitForSeconds(0.05f);
            }
            FinishTyping();
        }


    }

    private void FinishTyping()
    {
        StopAllCoroutines();
        isTyping = false;
        if (lines.Count > 0)
        {
            TMPText.text = lines.Dequeue();
        }
        if (lines.Count == 0 && currentDialogue.type == Dialogue.Type.Choices)
        {
            DisplayChoices();
        }
    }

    private void EndDialogue()
    {
        StopAllCoroutines();
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        if (currentDialogue.hasEventStopper)
        {
            dialogueEvents.StopEvent(currentDialogue.GetDialogueEvent());
        }
        // ---------------------------------------------------------------- Dialogue Box Animation and shit
        TMPText.text = "";
        currentDialogue = null;
        isTyping = false;
        isInDialogue = false;
    }

    private void DisplayChoices()
    {
        choicesPanel.SetActive(true);
        for (int i = 0; i < choices.Count; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].choiceName;
        }
    }

    private void LoadLines(Dialogue dialogue)
    {
        if (dialogue.lines?.Length > 0)
        {

            foreach (string line in dialogue.GetLines())
            {
                lines.Enqueue(line);
            }
        }
    }

    private void LoadChoices(Dialogue dialogue)
    {
        if (dialogue.choices?.Count > 0)
        {
            foreach (Choice choice in dialogue.choices)
            {
                choices.Add(choice);
            }
        }
    }

    public void OnClickChoice1()
    {
        EndDialogue();
        currentDialogue = choices[0].GetNextDialogue();
        PlayDialogue(currentDialogue);
    }

    public void OnClickChoice2()
    {
        EndDialogue();
        currentDialogue = choices[1].GetNextDialogue();
        PlayDialogue(currentDialogue);
    }

    public void OnClickChoice3()
    {
        EndDialogue();
        currentDialogue = choices[1].GetNextDialogue();
        PlayDialogue(currentDialogue);
    }

    private void InitializeChoiceButtons()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }

}
