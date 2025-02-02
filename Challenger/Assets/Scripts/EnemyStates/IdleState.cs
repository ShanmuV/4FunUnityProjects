using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleState : EnemyBaseState
{
    private int i = 0;
    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("In the Idle State");

        EnemyBaseState[] states = {enemy.laserAttackState, enemy.eruptionAttackState };

        /*int[] weights = { 30 , 15 };
        int sumOfWeights = weights.Sum();
        int rand = Random.Range(0, sumOfWeights);
        for(int i = 0; i < weights.Length; i++)
        {
            if(rand < weights[i])
            {
                enemy.SwitchState(states[i]);
                break;
            }
            rand -= weights[i];
        }*/

        enemy.SwitchState(states[i % states.Length]);
        i += 1;
    }

    public override void OnCollisionEnter(EnemyStateManager enemy, Collision collision)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        
    }

   
}
