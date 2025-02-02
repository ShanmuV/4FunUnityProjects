using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float disappearTime = 1f;

    private Rigidbody2D rb;

    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, disappearTime);
        currentVelocity = new Vector2(5f, 5f) * transform.up;
    }

    private void Update()
    {
        currentVelocity += new Vector2(50f, 50f) * transform.up * Time.deltaTime;
        rb.velocity = currentVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mr. Math"))
        {
            Destroy(gameObject);
        }
    }

}
