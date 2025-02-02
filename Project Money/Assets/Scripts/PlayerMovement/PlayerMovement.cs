using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Joystick joystick;

    public float speed;

    float dirX, dirY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        if(joystick.Horizontal > .15f)
        {
            dirX = speed;
        }
        else if(joystick.Horizontal < -.15f)
        {
            dirX = -speed;
        }
        else
        {
            dirX = 0f;
        }

        if(joystick.Vertical > .15f)
        {
            dirY = speed;
        }
        else if(joystick.Vertical < -.15f)
        {
            dirY = -speed;
        }
        else
        {
            dirY = 0f;
        }

        Move();

    }

    private void Move()
    {
        rb.velocity = new Vector2 (dirX, dirY);
    }

}
