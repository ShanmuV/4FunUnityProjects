using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent GameOver;

    public Path currentPath;

    public MovementSideWays movementSideways = new MovementSideWays();
    public MovementUpwards movementUpwards = new MovementUpwards();
    public PlayerHealth health;
    public HealthBar healthBar;

    private Movement currentMovementState;
    private bool movementBool = false;

    public float swapPathTimer = 5f;
    public float swapPathCounter = 5f;

    void Start()
    {
        transform.position = new Vector3(0, currentPath.transform.position.y, 0);
        transform.rotation = currentPath.transform.rotation;
        currentMovementState = movementSideways;
    }

    void Update()
    {
        currentMovementState.UpdateMovement(this);
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentPath.pathOnLeft != null && swapPathCounter <0f)
        {
            PathChange(currentPath.pathOnLeft);
            swapPathCounter = swapPathTimer;
        }
        if (Input.GetKeyDown(KeyCode.RightShift) && currentPath.pathOnRight != null && swapPathCounter <0f)
        {
            PathChange(currentPath.pathOnRight);
            swapPathCounter = swapPathTimer;
        }

        if(health.health <= 0)
        {
            GameOver?.Invoke();
        }
        swapPathCounter -= Time.deltaTime;
    }

    public void PathChange(Path path)
    {
        currentPath = path;
        transform.position = new Vector3(currentPath.transform.position.x, currentPath.transform.position.y, 0);
        transform.rotation = currentPath.transform.rotation;
        ChangeMovementState();
    }

    private void ChangeMovementState()
    {
        if (!movementBool)
        {
            currentMovementState = movementUpwards;
        }
        else
        {
            currentMovementState = movementSideways;
        }
        movementBool = ! movementBool;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Normal attacks"))
        {
            health.TakeDamage(this, 1);
        }
    }
}
