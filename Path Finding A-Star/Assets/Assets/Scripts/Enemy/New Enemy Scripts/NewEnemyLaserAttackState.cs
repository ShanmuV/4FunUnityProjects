using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyLaserAttackState : NewEnemyBaseState
{
    float stateTimer;
    public override void EnterState(NewEnemyStateManager enemy)
    {
        Debug.Log("In the Laser Attack State");
        stateTimer = 10f;
    }

    public override void OnCollisionEnter(NewEnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(NewEnemyStateManager enemy)
    {
        if(stateTimer > 0)
        {
            //do something
            stateTimer -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idleState);
        }
    }

}
