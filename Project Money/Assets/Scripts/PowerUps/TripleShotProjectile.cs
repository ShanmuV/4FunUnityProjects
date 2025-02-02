using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotProjectile : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * 15f, ForceMode2D.Impulse);
        Destroy(gameObject, 3f);
    }
}
