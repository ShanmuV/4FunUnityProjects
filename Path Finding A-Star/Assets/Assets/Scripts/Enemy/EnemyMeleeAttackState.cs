using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : EnemyBaseState
{
    public float StateTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("In the melee attack state");
        StateTimer = 10f;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (StateTimer > 0)
        {
            if (!enemy.enemyAttacks.isAttacking)
            {

                enemy.moveToTarget.StartMovingEnemy(enemy.gridMap);
            }

            if(Vector3.Distance(enemy.transform.position, enemy.target.position) < 2f)
            {
                enemy.rb.velocity = new Vector3(0f, enemy.rb.velocity.y, 0f);
                enemy.enemyAttacks.SwordAttack();
            }

            StateTimer -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idleState);
        }
    }
}
