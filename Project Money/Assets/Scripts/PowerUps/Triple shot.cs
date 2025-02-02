using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripleshot : PowerUp
{
    public float shootDelay = 3f;
    public float shootCounter = 3f;
    public GameObject prefab;
    public override float GetShootDelay()
    {
        return shootDelay;
    }

    public override void Initialize(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public override void Shoot(Player player)
    {
        if(player.closestEnemy != null)
        {
            Vector2 direction = player.transform.position - player.closestEnemy.transform.position;
            float degToRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            MonoBehaviour.Instantiate(prefab, player.muzzle.position, Quaternion.Euler(0, 0, degToRot + 75));
            MonoBehaviour.Instantiate(prefab, player.muzzle.position, Quaternion.Euler(0, 0, degToRot + 90));
            MonoBehaviour.Instantiate(prefab, player.muzzle.position, Quaternion.Euler(0, 0, degToRot + 105));
        }
    }

    public override void UpdateState(Player player)
    {
        if(shootCounter < 0f)
        {
            Shoot(player);
            shootCounter = shootDelay;
        }
        shootCounter -= Time.deltaTime;
    }
}
