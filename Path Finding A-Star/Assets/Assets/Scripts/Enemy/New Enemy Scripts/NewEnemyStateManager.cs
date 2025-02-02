using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyStateManager : MonoBehaviour
{

    NewEnemyBaseState currentState;

    [Header("States")]
    public NewEnemyIdleState idleState = new NewEnemyIdleState();
    public NewEnemyMeleeState meleeState = new NewEnemyMeleeState();
    public NewEnemyLaserAttackState laserAttackState = new NewEnemyLaserAttackState();
    public NewEnemyEruptionAttackState eruptionAttackState = new NewEnemyEruptionAttackState();

    [Header("Enemy Target Seeking")]
    public GridMap gridMap;
    public Transform Seeker;
    public Transform Target;

    [Header("Layer Masks")]
    public LayerMask groundLayer;
    public LayerMask platforms;


    [Header("Scripts")]
    public NewEnemyMovement enemyMovement;
    public PathFinding pathFinding;
    public NewEnemyAttacks enemyAttacks;


    [Header("")]
    public Rigidbody2D rb;





    bool isFlipped;
    public bool isAttacking = false;




    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Seeker = GetComponent<Transform>();
        enemyAttacks = GetComponent<NewEnemyAttacks>();

        currentState = meleeState;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Calling Update");
        currentState.UpdateState(this);
    }


    public void SwitchState(NewEnemyBaseState state)
    {
        state.EnterState(this);
        currentState = state;
    }


    public void FlipSprite()
    {
        if ((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
        {
            isFlipped = !isFlipped;
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;
        }
    }

}
