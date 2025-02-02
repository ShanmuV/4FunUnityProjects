using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private float dirX;

    [Header("Camera Shake")]
    [SerializeField] private Animator Camera;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem Damage;

    [Header("Damage")]
    [SerializeField] private AudioSource hit;

    private float coyoteTimer =0f;
    private float jumpBuffer = 0f;

    private int jumpCount = 2;

    private bool initialJump = false;

    private bool isFlipped = false;

    private bool gotAttacked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        Jump();

        

    }
    private void FixedUpdate()
    {
        if (!gotAttacked)
        {
            Move();
        }
        
    }

    private void Move()
    {
        if((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
        {
            Flip();
        }
        rb.velocity = new Vector3(dirX * 15f, rb.velocity.y);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            initialJump = false;
            jumpCount = 2;
            coyoteTimer = 0.2f;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffer = 0.2f;
        }
        else
        {
            jumpBuffer -= Time.deltaTime;
        }

        if (coyoteTimer > 0f && jumpBuffer > 0f && !initialJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 23f);
            jumpBuffer = 0;
        }

        if (initialJump && Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 23f);
            jumpBuffer = 0;
        }


        if (Input.GetButtonUp("Jump"))
        {
            jumpCount -= 1;
            initialJump = true;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
                coyoteTimer = 0f;
            }
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.7f, groundLayer);
    }

    private void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"))
        {
            hit.Play();
            Camera.Play("Mild_Shake");
            gotAttacked = true;
            Damage.Play();
            KnockBack();
        }
    }

    private async void KnockBack()
    {
        rb.velocity = new Vector2(20*enemy.localScale.x, rb.velocity.y);
        await Task.Delay(500);
        gotAttacked = false;
    }
}
