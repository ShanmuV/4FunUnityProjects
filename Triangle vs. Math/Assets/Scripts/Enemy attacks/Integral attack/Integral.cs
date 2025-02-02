using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Integral : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    public float attackSpeed;

    bool attack_start = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        Invoke("StartAttack", 1f);
    }


    void StartAttack()
    {
        Spin();
        rb.AddForce(-transform.up * attackSpeed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Borders"))
        {
            rb.velocity = Vector3.zero;
            Physics2D.IgnoreCollision(GetComponentInChildren<Collider2D>(), collision);
        }
    }

    private async void Spin()
    {
        if(!attack_start)
        {
            attack_start = true;
            animator.StopPlayback();
            animator.Play("Spin");
            await Task.Delay(2000);
            StopAttack();
        }
    }

    private void StopAttack()
    {
        rb.AddForce(transform.up * attackSpeed/2,ForceMode2D.Impulse);
        Destroy(gameObject, 0.7f);
    }

}
