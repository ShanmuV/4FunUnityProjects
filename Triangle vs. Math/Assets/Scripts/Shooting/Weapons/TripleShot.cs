using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : WeaponBase
{
    private float shootInterval = 0.25f;
    private float timer = 0.25f;


    public override void Shoot(PlayerAttackManager attack)
    {
        if (timer > shootInterval)
        {
            (MonoBehaviour.Instantiate(attack.bullet, attack.muzzle.position, attack.transform.rotation * Quaternion.Euler(0f,0f,20f))).GetComponent<TrailRenderer>().enabled = true;
            (MonoBehaviour.Instantiate(attack.bullet, attack.muzzle.position, attack.transform.rotation)).GetComponent<TrailRenderer>().enabled = true;
            (MonoBehaviour.Instantiate(attack.bullet, attack.muzzle.position, attack.transform.rotation * Quaternion.Euler(0f, 0f, -20f))).GetComponent<TrailRenderer>().enabled = true;
            timer = 0f;
        }
    }

    public override void UpdateTimer(PlayerAttackManager attack)
    {
        timer += Time.deltaTime;
    }

}
