using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        float[] weights = { 100, 50f, 20f };
        float weightSum = weights.Sum();
        float rand = Random.Range(0, weightSum);
        EnemyBaseState[] states = { enemy.meleeAttackState, enemy.laserAttackState, enemy.eruptionAttackState };

        for(int i = 0; i< weights.Length; i++)
        {
            if(rand < weights[i])
            {
                enemy.SwitchState(states[i]);
                break;
            }
            rand -= weights[i];
        }
    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {
        
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }
}
