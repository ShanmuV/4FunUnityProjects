using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;


    private float dirX, dirY;
    private bool isFlipped;
    private Rigidbody2D rb;

    private bool canMove = true;

    Quaternion flipped = Quaternion.Euler(new Vector3(0f, 180f, 0f));

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");
        if(canMove)
        {
            rb.velocity = new Vector2(dirX, dirY) * speed;
            Flip();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void Flip()
    {
        if(rb.velocity.x > 0 && isFlipped)
        {
            isFlipped = false;
            transform.rotation = Quaternion.identity; 
        }
        if(rb.velocity.x < 0 && !isFlipped)
        {
            isFlipped = true;
            transform.rotation = flipped;
        }
    }

    public void StopMoving()
    {
        canMove = false;
    }

    public void StartMoving()
    {
        canMove = true;
    }
}
