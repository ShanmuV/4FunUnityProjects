using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserAttackState : EnemyBaseState
{

    public float StateTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("In the Laser attack state");
        StateTimer = 10f;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        //await enemy.enemyAnimationController.PlayLaserAnimation();
        enemy.SwitchState(enemy.idleState);
    }
}
