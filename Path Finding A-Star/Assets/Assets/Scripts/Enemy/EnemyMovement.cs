using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] LayerMask groundLayer;

    private int jumpCount = 2;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void MoveRight()
    {
        rb.velocity = new Vector2(20f, rb.velocity.y);
    }

    public void MoveLeft()
    {
        rb.velocity = new Vector2(-20f, rb.velocity.y);
    }

    public void Jump()
    {
        if (isGrounded())
        {
            jumpCount = 2;
        }

        if (jumpCount > 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, 23f);
            jumpCount--;
            
        }
    }


    private bool isGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)transform.position, 1f, groundLayer);
    }
}
