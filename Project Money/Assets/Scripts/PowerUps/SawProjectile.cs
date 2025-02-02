using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawProjectile : MonoBehaviour
{
    public float angularVelocity = 100f;
    public float speed = 40f;
    public float change = 0.1f;
    public float counter = 0.1f;

    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();     
        rb.angularVelocity = angularVelocity;
    }
    private void Update()
    {
        if(counter < 0f)
        {
            rb.velocity = transform.up * speed;
            counter = change;
        }
        counter -= Time.deltaTime;
    }

}
