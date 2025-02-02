using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyMeleeState : NewEnemyBaseState
{
    private float stateTimer ;

    public override void EnterState(NewEnemyStateManager enemy)
    {
        stateTimer = 10f;
        Debug.Log("In the Melee State");
    }

    public override void OnCollisionEnter(NewEnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(NewEnemyStateManager enemy)
    {
        Debug.Log("Being Called");
        if(stateTimer > 0)
        {
            Debug.Log("In the melee update");
            if (!enemy.isAttacking)
            {
                Debug.Log("Moving");
                enemy.enemyMovement.MoveEnemy();
            }
            if(Vector3.Distance(enemy.transform.position, enemy.Target.position) < 2f)
            {
                enemy.rb.velocity = new Vector3(0f, enemy.rb.velocity.y);
                enemy.enemyAttacks.SwordAttack(enemy);
                //attack
            }

            stateTimer -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idleState);
        }
    }
}
