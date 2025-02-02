using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static float health = 100f;
    public HealthBar healthBar;

    Dictionary<string, int> collsionLayers = new Dictionary<string, int>()
    {
        {"Sword", 9 },
        {"Particle", 10 }
    };

    

    [SerializeField] PlayerAnimationController AnimationController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            health -= 5f;
            healthBar.UpdateHealth((int)health);
            Debug.Log("Health: " + health);
            AnimationController.Invincibilty(collsionLayers["Sword"]);
        }

        if (collision.CompareTag("Particle"))
        {
            health -= 5f;
            healthBar.UpdateHealth((int)health);
            Debug.Log("Health: " + health);
            AnimationController.Invincibilty(collsionLayers["Particle"]);
        }

    }
    private void Start()
    {
        healthBar.SetMaxHealth(100);
        healthBar.UpdateHealth((int)health);
    }

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
