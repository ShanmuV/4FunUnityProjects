using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    [SerializeField] private float maxHealth = 70f;
    [SerializeField] private float health;

    [SerializeField] private Slider healthBar;
    [SerializeField] private CanvasGroup healthBarCanvas;

    [SerializeField] private float healthBarTimer = 1f;


    void Start()
    {
        if (transform.position.x > 0)
        {
            speed *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(1,0,0) * speed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = health / maxHealth;
        ShowHealthBar();
    }

    private void ShowHealthBar()
    {
        StopAllCoroutines();
        healthBarCanvas.alpha = 1f;
        StartCoroutine(HealthBarActive());
        
    }

    private IEnumerator HealthBarActive()
    {
        yield return new WaitForSeconds(healthBarTimer);
        yield return healthBarCanvas.LeanAlpha(0, 1f);
    }
}
