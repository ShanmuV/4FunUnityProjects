using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackState : EnemyBaseState
{
    private bool isTeleporting = false;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("In the Laser Attack State");
        isTeleporting = false;
        enemy.Sword.enabled = false;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

        if (!isTeleporting)
        {
            
            enemy.rb.bodyType = RigidbodyType2D.Static;
            enemy.enemyCollider.enabled = false;
            isTeleporting = true;
            Teleport(enemy);
        }
    }

    private async void Teleport(EnemyStateManager enemy)
    {
        await enemy.enemyAnimationController.DoLaserAttack();
        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        enemy.enemyCollider.enabled = true;
        enemy.SwitchState(enemy.meleeAttackState);
        isTeleporting = false;
    }
}
