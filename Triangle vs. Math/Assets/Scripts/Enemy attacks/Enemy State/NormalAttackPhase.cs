using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackPhase : EnemyAttackBaseState
{
    private float spawnTimer = 0.5f;
    private float spawnCounter = 0f;

    private float phaseTimer = 15f;
    private float phaseCounter = 15f;
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
                enemy.Attack.DoNormalAttack();
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
