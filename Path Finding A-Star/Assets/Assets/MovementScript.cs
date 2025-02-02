using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite crouchSprite;

    [Header("Movement")]
    [SerializeField] private float maxGravity;
    [SerializeField] private float jumpForce;
    [SerializeField] private int jumpCount;
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private float wallSlideSpeed;

    /*[Header("Crouch")]
    [SerializeField] private float crouchMovementSpeed;
    [SerializeField] private Vector2 crouchColliderOffset;
    [SerializeField] private Vector2 crouchColliderSize;*/

    [Header ("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;

    [Header("Ground Collision")]
    [SerializeField] private LayerMask groundLayer;

    [Header("Wall Collision")]
    [SerializeField] private LayerMask wallLayer;



    //Coyote and jump buffer time vars
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    //Wall Jumping vars
    private bool isWallJumping = false;
    private bool initialJump = false;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.1f;
    private float wallJumpingTimeCounter;
    private float wallJumpingDuration = 0.4f;
    private int jumpCounter;
    private Vector2 wallJumpingForce = new Vector2(14f, 25f);

    //Dashing vars
    private bool CanDash = true;
    private bool isDashing = false;
    private float DashDuration = 0.15f;
    private float gravityScale;

    //Crouching vars
    //private bool canCrouch = true;
    //private bool isCrouching = false;

    private Vector2 normalColliderSize;
    private Vector2 normalColliderOffset;

    private float dirX;
    private float movementSpeed;
    private bool isFlipped = false;
    private bool isWallSliding = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        /*spriteRenderer = GetComponent<SpriteRenderer>();


        spriteRenderer.sprite = normalSprite;*/

        gravityScale = rb.gravityScale;

        movementSpeed = normalMovementSpeed;
        normalColliderSize = boxCollider.size;
        normalColliderOffset = boxCollider.offset;
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");

        Dash();
        Jump();
        //WallSlide();
        //WallJump();
        //Crouch();
    }

    private void FixedUpdate()
    {
        Move();
        ClampGravity();
    }

    private void Move()
    {
        if (isWallJumping || isDashing) return;
        //FlipPlayer();
        rb.velocity = new Vector2(dirX * movementSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (isWallJumping || isDashing) return;
        if (isGrounded())
        {
            initialJump = false;
            jumpCounter = jumpCount;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0 && !initialJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
        }

        if(initialJump && Input.GetButtonDown("Jump") && jumpCounter>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;

        }


        if (Input.GetButtonUp("Jump"))
        {
            jumpCounter -= 1;
            initialJump = true;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.3f);
                coyoteTimeCounter = 0f;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
    }

    private void ClampGravity()
    {
        if (rb.velocity.y < maxGravity) rb.velocity = new Vector2(rb.velocity.x, maxGravity);
    }

    private bool IsWalled()
    {
        return ((Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, 0.7f, wallLayer) && dirX < 0) || (Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, 0.7f, wallLayer) && dirX > 0));
    }

    private void FlipPlayer()
    {
        if (dirX > 0 && isFlipped || dirX < 0 && !isFlipped)
        {
            isFlipped = !isFlipped;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void WallJump()
    {
        if (isWallJumping || isDashing) return;

        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingTimeCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && !isGrounded() && wallJumpingTimeCounter > 0)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingForce.x, wallJumpingForce.y);
            wallJumpingTimeCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFlipped = !isFlipped;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
        FlipPlayer();
    }

    private void WallSlide()
    {
        if (isWallJumping || isDashing) return;

        if (IsWalled() && !isGrounded() && dirX != 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Dash()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && CanDash)
        {
            isDashing = true;
            CanDash = false;
        }
        if (isDashing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            Invoke(nameof(StopDashing), DashDuration);
            Invoke(nameof(DashCoolDown), dashCooldown);

        }

    }

    private void StopDashing()
    {
        isDashing = false;
        rb.velocity = new Vector2(0f, rb.velocity.y);
        rb.gravityScale = gravityScale;
    }

    private void DashCoolDown()
    {
        CanDash = true;
    }

    /*private void Crouch()
    {
        if (!isGrounded() || isWallSliding) return;

        if (Input.GetKeyDown(KeyCode.LeftControl) && canCrouch)
        {
            isCrouching = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        if (isCrouching)
        {
            spriteRenderer.sprite = crouchSprite;
            movementSpeed = crouchMovementSpeed;
            boxCollider.size = crouchColliderSize;
            boxCollider.offset = crouchColliderOffset;
        }
        else
        {
            spriteRenderer.sprite = normalSprite;
            movementSpeed = normalMovementSpeed;
            boxCollider.size = normalColliderSize;
            boxCollider.offset = normalColliderOffset;
        }
    }*/
}
