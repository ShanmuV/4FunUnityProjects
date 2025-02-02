using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEruptionAttackState : EnemyBaseState
{

    public float StateTimer;

    public override void EnterState(EnemyStateManager enemy)
    {
        StateTimer = 10f;
        Debug.Log("In the Eruption Attack State.");
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if(StateTimer > 0)
        {
            //do the eruption attack
            StateTimer -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idleState);
        }
    }
}
