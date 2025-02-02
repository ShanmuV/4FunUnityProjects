using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanAttackPhase : EnemyAttackBaseState
{
    private float spawnTimer = 1.5f;
    private float spawnCounter = 0f;

    private float phaseTimer = 12f;
    private float phaseCounter = 12f;

    public override void EnterPhase(MrMath enemy)
    {
        phaseCounter = phaseTimer;
    }

    public override void UpdatePhase(MrMath enemy)
    {
        if(phaseCounter > 0)
        {
            if(spawnCounter < 0f)
            {
                enemy.Attack.DoTanAttack();
                spawnCounter = spawnTimer;
            }
            spawnCounter -= Time.deltaTime;
            phaseCounter -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idle);
            phaseCounter = phaseTimer;
        }
    }

}
