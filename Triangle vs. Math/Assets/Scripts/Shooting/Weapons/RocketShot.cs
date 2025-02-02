using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShot : WeaponBase
{
    private float shootInterval = 0.75f;
    private float timer = 0.75f;

    public override void Shoot(PlayerAttackManager attack)
    {
        if(timer > shootInterval)
        {
            MonoBehaviour.Instantiate(attack.rocket, attack.muzzle.position, attack.transform.rotation);
            timer = 0f;
        }
    }

    public override void UpdateTimer(PlayerAttackManager attack)
    {
        timer += Time.deltaTime;
    }

}
