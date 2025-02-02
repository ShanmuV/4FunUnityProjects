using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : EnemyBaseState
{
    float StateTimer;
    public override void EnterState(EnemyStateManager enemy)
    {
        StateTimer = 10f;
        enemy.Sword.enabled = true;
        Debug.Log("In the Melee State");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(StateTimer > 0)
        {
            if (Vector3.Distance(enemy.transform.position, enemy.target.position) > 2f)
            {
                enemy.enemyMovement.StartMoving();
            }
            else
            {
                enemy.rb.velocity = new Vector2(0f, enemy.rb.velocity.y);
                enemy.enemyAnimationController.MeleeAttack();
            }
            StateTimer -= Time.deltaTime;
        }
        else
        {
            enemy.enemyMovement.StopMovingEnemy();
            enemy.SwitchState(enemy.idleState);
        }
    }
}
