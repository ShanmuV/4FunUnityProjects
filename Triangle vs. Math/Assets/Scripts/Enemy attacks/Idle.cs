using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyAttackBaseState
{
    float restTime = 2f;
    float counter = 2f;

    public override void EnterPhase(MrMath enemy)
    {
        counter = restTime;
    }

    public override void UpdatePhase(MrMath enemy)
    {
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            if(enemy.playerAlive)
            {
                PickPhase(enemy);
            }
        }
    }

    private void PickPhase(MrMath enemy)
    {
        List<EnemyAttackBaseState> phases = enemy.GetEnemyPhases();
        int index = Random.Range(0, phases.Count);
        enemy.SwitchState(phases[index]);
    }


}
