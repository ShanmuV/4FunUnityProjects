using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] PlayerHealth playerHealth;

    public List<Heart> hearts = new List<Heart>();

    public Heart healingHeart;

    private void Start()
    {
        InitializeHearts();
    }

    public void InitializeHearts()
    {
        for(int i = 0; i < playerHealth.maxHealth; i++)
        {
            GameObject newHeart = Instantiate(heart, gameObject.transform);
            hearts.Add(newHeart.GetComponent<Heart>());
        }
        UpdateHearts();

    }

    public void UpdateHearts()
    {
        for(int i = hearts.Count - 1;i >= 0;i--)
        {
            if(i < playerHealth.health)
            {
                hearts[i].SetHeart(HeartStatus.fullHeart);
                ClearFill(hearts[i]);
            }
            else
            {
                hearts[i].SetHeart(HeartStatus.emptyHeart);
                ClearFill(healingHeart);
                healingHeart = hearts[i];
            }
        }
    }

    private void ClearFill(Heart heart)
    {
        if(heart != null)
        heart.Fill.fillAmount = 0f;
    }

}
