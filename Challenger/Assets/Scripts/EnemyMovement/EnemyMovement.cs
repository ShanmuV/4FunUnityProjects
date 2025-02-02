using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    PathFindingCopy pathfinding;
    Rigidbody2D rb;


    [SerializeField] private GridMap gridMap;
    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask PlatformLayer;
    [SerializeField] private LayerMask GroundLayer;

    private bool movingEnemy = false;

    private bool isFlipped = false;

    private int jumpCount;

    private void Start()
    {
        pathfinding = GetComponent<PathFindingCopy>();
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void Update()
    {
        if (!movingEnemy)
        {
            movingEnemy = true;
            StartCoroutine(MoveEnemy());
        }
    }*/

    private IEnumerator MoveEnemy()
    {
        while (gridMap.path != null && gridMap.path.Count > 0)
        {
            Node current = gridMap.path[0];
            gridMap.path.RemoveAt(0);


            if (transform.localPosition.x < current.GetCenterPosition().x)
            {
                MoveRight();
            }
            else if (transform.localPosition.x > current.GetCenterPosition().x)
            {
                MoveLeft();
            }

            if((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
            {
                Flip();
            }

            if (current.worldPos.y-2f > gameObject.transform.position.y && !IsWallAbove())
            {
                //Debug.Log("Current: "+current.worldPos.y+"\nEnemy: "+transform.position.y);
                Jump();
                yield return new WaitForSeconds(0.7f);
            }

        }
        yield return new WaitForSeconds(0.25f);
        Invoke("StopMovingEnemy", 0.2f);
    }

    private void Flip()
    {
        isFlipped = !isFlipped;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void StopMovingEnemy()
    {
        StopAllCoroutines();
        movingEnemy = false;
    }

    private bool IsWallAbove()
    {
        //Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 4.5f),Color.yellow,0.3f);
        return Physics2D.Raycast(transform.position, Vector2.up, 4.5f, PlatformLayer);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position, 0.7f, GroundLayer);
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
        if (IsGrounded())
        {
            jumpCount = 2;
        }

        if (jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.3f, 23f);
            jumpCount--;

        }
    }

    public void StartMoving()
    {
        if (!movingEnemy)
        {
            movingEnemy = true;
            StartCoroutine(MoveEnemy());
        }
    }

}
