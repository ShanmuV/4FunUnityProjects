using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class NormalWeapon : WeaponBase
{

    private float shootInterval = 0.2f;
    private float timer = 0.5f;
    public override void Shoot(PlayerAttackManager attack)
    {
        if(timer > shootInterval)
        {
            MonoBehaviour.Instantiate(attack.bullet, attack.muzzle.position, attack.transform.rotation);
            timer = 0f;
        }
    }

    public override void UpdateTimer(PlayerAttackManager attack)
    {
        timer += Time.deltaTime;
    }
}

