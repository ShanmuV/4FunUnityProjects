using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanAttacks : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    [SerializeField] private float attackSpeed;

    bool attack_start = false;

    private void Awake()
    {
        Invoke("StartAttack", 1f);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void StartAttack()
    {
        rb.AddForce(-transform.up * attackSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Borders"))
        {
            Invoke("Split", 0.5f);
            rb.velocity = Vector3.zero;
            
        }
    }

    void Split()
    {
        if (!attack_start)
        {   
            animator.StopPlayback();
            attack_start = true;
            animator.Play("1st_split");
            Invoke("DestroyObject", animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        }
        
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }


}
