using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    public static NewDialogueManager instance;

    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI textBox;

    private Dialogue currentDialogue;
    private Queue<string> currentLines;
    private string currLine;

    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject[] choiceButtons = new GameObject[3];
    private List<Choice> choices;

    private Object nearestObject;

    private bool isInDialogue;
    private bool isTyping;

    private NewPlayerManager playerManager;

    private void Start()
    {
        if(NewPlayerManager.instance != null)
        {
            playerManager = NewPlayerManager.instance;
        }
        else
        {
            Debug.LogError("From Dialogue Manager, the player manager instance is not found");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nearestObject != null && !isInDialogue)
            {
                StartDialogue(nearestObject.GetDialogue());
            }
            else if(isInDialogue && isTyping)
            {
                FinishTyping();
            }
            else if(isInDialogue && !isTyping)
            {
                StartCoroutine(PlayNextLine());
            }
        }
    }

    private void StartDialogue(Dialogue dialogue)
    {
        isInDialogue = true;
        playerManager.playerMovement.StopMoving();
        currentDialogue = dialogue;
        currentLines.Clear();
        currentLines = new Queue<string>(currentDialogue.GetLines());
        dialogueCanvas.SetActive(true);
        StartCoroutine(PlayNextLine());
    }

    private IEnumerator PlayNextLine()
    {
        if(currentLines.Count == 0)
        {
            switch (currentDialogue.type)
            {
                case Dialogue.Type.Normal:
                    EndDialogue();
                    break;
                case Dialogue.Type.Choices:
                    break;
                default: break;
            }
            yield return null;
        }
        else
        {
            isTyping= true;
            currLine = currentLines.Dequeue();
            textBox.text = "";
            for(int i = 0; i < currLine.Length; i++)
            {
                textBox.text+= currLine[i];
                yield return new WaitForSeconds(0.05f);
            }
            FinishTyping();
        }
    }

    private void FinishTyping()
    {
        isTyping = false;
        textBox.text = currLine;
        currLine = "";
        if(currentDialogue.type == Dialogue.Type.Choices)
        {
            ShowChoices();
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        isInDialogue = false;
        dialogueCanvas.SetActive(false);
        playerManager.playerMovement.StartMoving();
    }

    private void ShowChoices()
    {
        choices = currentDialogue.choices;
        choicePanel.SetActive(true);
        ClearChoices();
        for(int i =0; i<choices.Count; i++)
        {
            choiceButtons[i].SetActive(true);
            choiceButtons[i].GetComponent<TextMeshPro>().text = choices[i].ToString();
        }

    }

    private void ClearChoices()
    {
        foreach(GameObject obj in choiceButtons)
        {
            obj.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objects"))
        {
            nearestObject = collision.gameObject.GetComponent<Object>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Objeccts"))
        {
            nearestObject = null;
        }
    }

    private void OnClickChoice1()
    {
        EndDialogue();
        StartDialogue(choices[0].nextDialogue);
    }

    private void OnClickChoice2()
    {
        EndDialogue();
        StartDialogue(choices[1].nextDialogue);
    }

    private void OnClickChoice3()
    {
        EndDialogue();
        StartDialogue(choices[2].nextDialogue);
    }
}
