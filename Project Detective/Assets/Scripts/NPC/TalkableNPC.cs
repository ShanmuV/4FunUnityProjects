using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkableNPC : MonoBehaviour
{
    public RectTransform textBubble;
    public Transform player;
    public float minDistanceForText = 2f;

    public bool isNear;
    public bool textBubbleOn;

    LTDescr bounceBack;

    Vector2 originalScale;

    private void Start()
    {
        originalScale = textBubble.localScale;
        textBubble.localScale = Vector2.zero;
    }


    private void Update()
    {
        if (isNear)
        {
            TextBubbleAppear();
        }
        else
        {
            TextBubbleDisappear();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNear = false;
        }
    }


    private void TextBubbleAppear()
    {
        if (!textBubbleOn)
        {
            textBubbleOn = true;
            textBubble.LeanScale(originalScale, 0.25f).setEaseOutCubic();
        }
    }

    private void TextBubbleAnim()
    {
        bounceBack = textBubble.LeanScale(originalScale * 1.05f, 1f).setEaseInOutCirc().setLoopPingPong();
    }

    private void TextBubbleDisappear()
    {
        if (textBubbleOn)
        {
            textBubbleOn = false;
            textBubble.LeanScale(Vector2.zero, 0.25f).setEaseInCubic();
        }
    }

}
