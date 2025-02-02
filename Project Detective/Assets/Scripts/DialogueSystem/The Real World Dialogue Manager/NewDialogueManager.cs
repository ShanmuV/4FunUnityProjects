using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueManager : MonoBehaviour
{
    /*    [SerializeField] private DialoguePanel dialoguePanel;
        [SerializeField] private GameObject choicesPanel;
        [SerializeField] private GameObject[] buttons = new GameObject[3];
        [SerializeField] private TextMeshProUGUI text;

        private NewPlayerManager playerManager;

        private List<Choice> choices = new List<Choice>();

        private bool isPlayerTouching;
        private Object nearestObject;
        private Dialogue currentDialogue;

        private bool isTyping;
        private bool isInDialogue;

        private Queue<string> lines;
        private string currLine;

        private void Start()
        {
            if (DialoguePanel.instance != null)
            {
                dialoguePanel = DialoguePanel.instance;
            }
            else
            {
                Debug.Log("Dialogue panel singleton not found");
            }
            if(NewPlayerManager.instance != null)
            {
                playerManager = NewPlayerManager.instance;
            }
            else
            {
                Debug.LogError("Player Manager instance not found");
            }
        }

        void Update()
        {
            if(isPlayerTouching && Input.GetKeyDown(KeyCode.Return))
            {
                if(!isInDialogue)
                {
                    currentDialogue = nearestObject?.GetDialogue();
                    StartDialogue();
                }
                else if(isInDialogue)
                {
                    StartCoroutine(PlayNextLine());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Objects"))
            {
                isPlayerTouching = true;
                nearestObject = collision.gameObject.GetComponent<Object>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Objects"))
            {
                isPlayerTouching = false;
                nearestObject = null;
                currentDialogue = null;
            }
        }

        private void StartDialogue()
        {

            if(currentDialogue!= null)
            {
                isInDialogue = true;
                dialoguePanel.dialoguePanel.SetActive(true);
                lines = new Queue<string>(currentDialogue.GetLines());
                StartCoroutine(PlayNextLine());
            }
        }

        private IEnumerator PlayNextLine()
        {
            if(lines.Count==0 )
            {
                if (currentDialogue.type == Dialogue.Type.Normal) EndDialogue();
            }

            if(lines?.Count > 0 && !isTyping)
            {
                isTyping = true;
                currLine = lines.Dequeue();
                text.text = "";
                for(int i = 0; i < currLine.Length; i++)
                {
                    text.text += currLine[i];
                    yield return new WaitForSeconds(0.025f);
                }
                Finishtyping();
            }
            else if(isTyping)
            {
                Finishtyping();
            }
        }

        private void Finishtyping()
        {
            StopAllCoroutines();
            isTyping = false;
            text.text = currLine;

            if(currentDialogue.type == Dialogue.Type.Choices)
            {
                playerManager.playerMovement.StopMoving();
                ShowChoices();
            }
        }

        private void ShowChoices()
        {
            choicesPanel.SetActive(true);
            choices = currentDialogue.choices;
            for(int i=0; i < choices.Count; i++)
            {
                buttons[i].GetComponent<TextMeshProUGUI>().text = choices[i].ToString();
                buttons[i].SetActive(true);
            }
        }
        public void OnClickChoice1()
        {
            EndDialogue();
            currentDialogue = choices[0].GetNextDialogue();
            StartDialogue();
        }

        public void OnClickChoice2()
        {
            EndDialogue();
            currentDialogue = choices[1].GetNextDialogue();
            StartDialogue();
        }

        public void OnClickChoice3()
        {
            EndDialogue();
            currentDialogue = choices[1].GetNextDialogue();
            StartDialogue();
        }

        private void EndDialogue()
        {
            isTyping = false;
            isInDialogue = false;
            ClearChoices();
            playerManager.playerMovement.StartMoving();
            dialoguePanel.dialoguePanel.SetActive(false);
            lines = null;
        }

        private void ClearChoices()
        {
            foreach(GameObject obj in buttons)
            {
                obj.SetActive(false);
            }
        }*/


    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textBox;

    private Dialogue currentDialogue;
    private Queue<string> currentLines;
    private string currLine;

    [SerializeField] private GameObject choicePanel;
    [SerializeField] private GameObject[] choiceButtons;
    private List<Choice> choices;

    private Object nearestObject;

    private bool isInDialogue;
    private bool isTyping;

    private NewPlayerManager playerManager;
    private DialoguePanel dialoguePanelInstance;

    private void Start()
    {
        if (NewPlayerManager.instance != null)
        {
            playerManager = NewPlayerManager.instance;
        }
        else
        {
            Debug.LogError("From Dialogue Manager, the player manager instance is not found");
        }

        if (DialoguePanel.instance != null)
        {
            dialoguePanelInstance = DialoguePanel.instance;

            dialoguePanel = dialoguePanelInstance.dialoguePanel;
            textBox = dialoguePanelInstance.textBox;
            choicePanel = dialoguePanelInstance.choicePanel;
            choiceButtons = dialoguePanelInstance.choiceButtons;
        }
        else
        {
            Debug.LogError("From DialogueManager, Dialogue panel instance not found");
        }

        choiceButtons[0].GetComponent<Button>().onClick.AddListener(OnClickChoice1);
        choiceButtons[1].GetComponent<Button>().onClick.AddListener(OnClickChoice2);
        choiceButtons[2].GetComponent<Button>().onClick.AddListener(OnClickChoice3);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (nearestObject != null && !isInDialogue)
            {
                StartDialogue(nearestObject.GetDialogue());
            }
            else if (isInDialogue && isTyping)
            {
                FinishTyping();
            }
            else if (isInDialogue && !isTyping)
            {
                StartCoroutine(PlayNextLine());
            }
        }
    }

    private void StartDialogue(Dialogue dialogue)
    {
        if (dialogue == null) return;

        isInDialogue = true;
        playerManager.playerMovement.StopMoving();
        this.currentDialogue = dialogue;
        currentLines = new Queue<string>(currentDialogue.GetLines());
        dialoguePanel.SetActive(true);
        StartCoroutine(PlayNextLine());
    }

    private IEnumerator PlayNextLine()
    {
        if (this.currentLines.Count == 0)
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
        }
        else if(currentLines.Count>0 && !isTyping)
        {
            isTyping = true;
            currLine = currentLines.Dequeue();
            textBox.text = "";
            for (int i = 0; i < currLine.Length; i++)
            {
                textBox.text += currLine[i];
                yield return new WaitForSeconds(0.05f);
            }
            FinishTyping();
        }
    }

    private void FinishTyping()
    {
        StopAllCoroutines();
        textBox.text = currLine;
        if (currentDialogue.type == Dialogue.Type.Choices)
        {
            ShowChoices();
        }
        isTyping = false;
    }

    private void EndDialogue()
    {
        isInDialogue = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        playerManager.playerMovement.StartMoving();
    }

    private void ShowChoices()
    {
        choices = new List<Choice>(currentDialogue.choices);
        choicePanel.SetActive(true);
        ClearChoices();
        for (int i = 0; i < choices.Count; i++)
        {
            choiceButtons[i].SetActive(true);
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].ToString();
        }

    }

    private void ClearChoices()
    {
        foreach (GameObject obj in choiceButtons)
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
        if (collision.CompareTag("Objects"))
        {
            nearestObject = null;
        }
    }

    public void OnClickChoice1()
    {
        EndDialogue();
        StartDialogue(choices[0].nextDialogue);
    }

    public void OnClickChoice2()
    {
        EndDialogue();
        StartDialogue(choices[1].nextDialogue);
    }

    public void OnClickChoice3()
    {
        EndDialogue();
        StartDialogue(choices[2].nextDialogue);
    }
}
