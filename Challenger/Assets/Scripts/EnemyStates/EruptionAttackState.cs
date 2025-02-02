using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EruptionAttackState : EnemyBaseState
{
    private bool isAttacking;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("In the Eruption Attack State");
        isAttacking = false;
        enemy.Sword.enabled = false;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (!isAttacking)
        {
            enemy.rb.bodyType = RigidbodyType2D.Static;
            enemy.enemyCollider.enabled = false;
            isAttacking = true;
            BeginAttack(enemy);

        }
    }

    private async void BeginAttack(EnemyStateManager enemy)
    {
        await enemy.enemyAnimationController.EruptionAttack();
        enemy.rb.bodyType = RigidbodyType2D.Dynamic;
        enemy.enemyCollider.enabled = true;
        enemy.SwitchState(enemy.meleeAttackState);
        isAttacking = false;

    }

}
