using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int health = 3;

    public HealthBar healthBar;

    public float regenTime = 10f;
    public float regenCounter = 0;

    public void TakeDamage(PlayerManager player, int damage)
    {
        health -= damage;
        healthBar.UpdateHearts();
        Debug.Log("Health : " + health);
    }

    private void Update()
    {
        if (health < maxHealth)
        {
            regenCounter += Time.deltaTime;
            if(healthBar.healingHeart != null)
            healthBar.healingHeart.Fill.fillAmount = regenCounter / regenTime;
        }
        else
        {
            regenCounter = 0f;
            healthBar.healingHeart = null;
        }

        if (regenCounter >= regenTime)
        {
            Regen();
            regenCounter = 0f;
        }
    }

    public void Regen()
    {
        health++;
        healthBar.UpdateHearts();
    }

}
