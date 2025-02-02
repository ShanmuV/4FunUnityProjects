using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableNPC : MonoBehaviour
{
    [SerializeField] private PlayerManager player;
    [SerializeField] private GameObject exclamation;

    [Header("Dialogue Options")]
    public bool canInteract;
    public Dialogue dialogue;

    bool isExclamationAnimating;
    LTDescr exclaimAnim;

    public float distanceFromPlayer;

    private void Start()
    {
        exclamation.SetActive(false);
    }

    private void Update()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.transform.position);
    }

    public void CanInteractWithPlayer()
    {
        canInteract = true;
        if(!isExclamationAnimating) AnimateExclamation();
    }

    public void CantInteractWithPlayer()
    {
        canInteract = false;
        StopAnimateExclamation();
    }

    public void AnimateExclamation()
    {
        exclamation.SetActive(true);
        isExclamationAnimating = true;
        exclaimAnim = exclamation.transform.LeanMoveLocal(new Vector2(0,0.3f),1f).setEaseInOutSine().setLoopPingPong();
    }
    
    public void StopAnimateExclamation()
    {
        exclamation.transform.localPosition = new Vector2(0, 0);
        exclamation.SetActive(false);
        isExclamationAnimating = false;
        if (exclaimAnim != null) LeanTween.cancel(exclaimAnim.id);
    }

    public void ChangeDialogue(Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.interactableNPCs.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D colllsion)
    {
        if (colllsion.CompareTag("Player"))
        {
            player.interactableNPCs.Remove(this);
        }
    }
}
