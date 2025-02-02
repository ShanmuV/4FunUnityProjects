using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float MaxHealth = 1500f;
    public float health;
    public Image Fill;

    private void Awake()
    {
        health = MaxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        Fill.fillAmount = health/MaxHealth;
    }

}
