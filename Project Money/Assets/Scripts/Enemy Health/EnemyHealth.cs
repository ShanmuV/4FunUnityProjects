using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 5;
    public ParticleSystem damage;
    public Collider2D coll;
    public SpriteRenderer sprite;
    public EnemyMovement enemyMovement;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            coll.enabled = false;
            enemyMovement.enabled = false;
            sprite.enabled = false;
            if (!damage.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            TakeDamage(1);
            damage.Play();
        }
    }

    private void TakeDamage(int damage)
    {
        health-=damage;
    }
}
