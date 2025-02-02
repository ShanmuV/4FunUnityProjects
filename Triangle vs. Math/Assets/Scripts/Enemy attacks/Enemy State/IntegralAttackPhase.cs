using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntegralAttackPhase : EnemyAttackBaseState
{

    float integTimer = 4f;
    float integCounter = 0f;

    float normalAttTimer = 1f;
    float normalAttCounter = 1f;

    float phaseTimer = 10f;
    float phaseCounter = 10f;

    public override void EnterPhase(MrMath enemy)
    {
        phaseCounter = phaseTimer;
    }

    public override void UpdatePhase(MrMath enemy)
    {
        if(phaseCounter > 0)
        {
            if(integCounter <= 0f)
            {
                integCounter = integTimer;
                enemy.Attack.DoIntegralAttack();
            }

            if(normalAttCounter <= 0f)
            {
                normalAttCounter = normalAttTimer;
                enemy.Attack.DoNormalAttack();
            }

            integCounter -= Time.deltaTime;
            normalAttCounter -= Time.deltaTime;
            phaseCounter -= Time.deltaTime;
        }
        else
        {
            enemy.SwitchState(enemy.idle);
            phaseCounter = phaseTimer;
        }
    }


}
