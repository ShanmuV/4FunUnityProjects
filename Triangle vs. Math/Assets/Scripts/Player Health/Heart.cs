using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Image imageComponent;

    public Sprite fullHeart, emptyHeart;

    public Image Fill;


    private void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    public void SetHeart(HeartStatus status)
    {
        if(status == HeartStatus.emptyHeart)
        {
            imageComponent.sprite = emptyHeart;
        }
        else
        {
            imageComponent.sprite= fullHeart;
        }
    }

}

public enum HeartStatus
{
    fullHeart,
    emptyHeart
}
