using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float bullet_disappear_time = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        Destroy(gameObject, bullet_disappear_time);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bullet_disappear_time = 2f;
    }

    private void Update()
    {
        rb.velocity = transform.up * 20f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mr. Math"))
        {
            Destroy(gameObject);
        }
    }

}
