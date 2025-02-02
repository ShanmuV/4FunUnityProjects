using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewEnemyIdleState : NewEnemyBaseState
{

    public override void EnterState(NewEnemyStateManager enemy)
    {
        float[] weights = { 100, 50f, 20f };
        float weightSum = weights.Sum();
        float rand = Random.Range(0, weightSum);
        NewEnemyBaseState[] states = { enemy.meleeState, enemy.laserAttackState, enemy.eruptionAttackState };

        for (int i = 0; i < weights.Length; i++)
        {
            if (rand < weights[i])
            {
                enemy.SwitchState(states[i]);
                break;
            }
            rand -= weights[i];
        }

    }

    public override void OnCollisionEnter(NewEnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(NewEnemyStateManager enemy)
    {
        
    }
}
