using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darts : Weapon
{
    GameObject projectile;

    int damage = 1;

    public float shootDelay = 0.5f;

    public override void Initialize(GameObject projectile)
    {
        this.projectile = projectile;
    }

    public override void Shoot(Player player)
    {
        Vector2 direction = player.transform.position - player.closestEnemy.transform.position;
        float degToRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        MonoBehaviour.Instantiate(projectile,player.muzzle.position, Quaternion.Euler(0,0,degToRot + 90));
    }

    public override float GetShootDelay()
    {
        return shootDelay;
    }
}
