using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    public static DialoguePanel instance;

    public GameObject dialoguePanel;
    public TextMeshProUGUI textBox;
    public GameObject choicePanel;
    public GameObject[] choiceButtons = new GameObject[3];


    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        } 
    }
}
