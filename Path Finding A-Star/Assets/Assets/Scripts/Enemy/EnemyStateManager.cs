using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;

    public EnemyIdleState idleState = new EnemyIdleState();
    public EnemyMeleeAttackState meleeAttackState = new EnemyMeleeAttackState();
    public EnemyLaserAttackState laserAttackState = new EnemyLaserAttackState();
    public EnemyEruptionAttackState eruptionAttackState = new EnemyEruptionAttackState();

    public GridMap gridMap;
    public Transform target;

    public EnemyAttacks enemyAttacks;
    public Animator SwordAnimationController;
    public Collider2D SwordCollider;

    public Rigidbody2D rb;
    public MoveToTarget moveToTarget;

    public EnemyAnimationController enemyAnimationController;

    bool isFlipped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveToTarget = GetComponent<MoveToTarget>();
        enemyAttacks = GetComponent<EnemyAttacks>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();



        currentState = meleeAttackState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        FlipSprite();

    }

    public void SwitchState(EnemyBaseState state)
    {
        if (currentState == state) { return; }

        currentState = state;
        currentState.EnterState(this);
    }

    public void FlipSprite()
    {
        if((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
        {
            isFlipped = !isFlipped;
            Vector3 localScale = transform.localScale;
            localScale.x = - localScale.x;
            transform.localScale = localScale;
        }
    }
}
