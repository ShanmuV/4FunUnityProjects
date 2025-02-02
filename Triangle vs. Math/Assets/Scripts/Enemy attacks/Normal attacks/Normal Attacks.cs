using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttacks : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private float attackSpeed;
    [SerializeField] private EnemyAttackManager enemyAttackManager;

    private void Awake()
    {
        Invoke("StartAttack", 1.5f);
        Invoke("DestroyObject", 3f);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void StartAttack()
    {
        rb.AddForce(-transform.up *  attackSpeed, ForceMode2D.Impulse);
    }


    void DestroyObject()
    {
        Destroy(gameObject);
    }

}
