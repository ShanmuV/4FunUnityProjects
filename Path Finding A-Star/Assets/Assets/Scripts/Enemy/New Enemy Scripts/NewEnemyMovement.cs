using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NewEnemyMovement : MonoBehaviour 
{
    PathFinding pathFinding;
    Rigidbody2D rb;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask platforms;

    [Header("Target Seeking")]
    [SerializeField] private Transform seeker;
    [SerializeField] private Transform target;





    private int jumpCount = 2;

    private bool isMoving = false;

    /*public NewEnemyMovement(NewEnemyStateManager enemy)
    {
        this.rb = enemy.rb;
        this.pathFinding = enemy.pathFinding;
        this.groundLayer = enemy.groundLayer;
        this.platforms = enemy.platforms;
        this.seeker = enemy.Seeker;
        this.target = enemy.Target;
    }*/

    private void Start()
    {
        pathFinding = GetComponent<PathFinding>();
        rb = GetComponent<Rigidbody2D>();
    }



    private void MoveRight()
    {
        rb.velocity = new Vector2(20f, rb.velocity.y);
    }

    private void MoveLeft()
    {
        rb.velocity = new Vector2(-20f, rb.velocity.y);
    }

    private void Jump()
    {
        if (isGrounded())
        {
            jumpCount = 2;
        }

        if (jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, 23f);
            jumpCount--;

        }
    }


    private bool isGrounded()
    {
        return Physics2D.OverlapCircle((Vector2)seeker.transform.position, 1f, groundLayer);
    }

    public async void MoveToTarget()
    {
        Debug.Log("Callinggggggggg");
        List<Vector3> path = pathFinding.FindPath(seeker.position, target.position);
        Debug.Log(path);


        while (path.Count > 0)
        {
            Vector3 current = path[0];
            path.RemoveAt(0);

            if(current.x > seeker.transform.position.x)
            {
                MoveRight();
            }
            else if(current.x < seeker.transform.position.x)
            {
                MoveLeft();
            }

            if(current.y > seeker.transform.position.y && !IsWallAbove())
            {
                Jump();
                await Task.Delay(700);
            }
        }

        Invoke("StopMovingEnemy", 0.2f);
    }

    private bool IsWallAbove()
    {
        return Physics2D.Raycast(seeker.transform.position, Vector2.up, 5f, platforms);
    }

    private void StopMovingEnemy()
    {
        isMoving = false;
    }

    public void MoveEnemy()
    {
        if(!isMoving)
        {
            MoveToTarget();
            isMoving = true;
        }
        
    }

}
