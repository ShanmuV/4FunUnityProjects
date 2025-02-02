using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBot
{
    public Collider2D[] GetEnemiesInRange(Player player)
    {
        Collider2D[] arr =  Physics2D.OverlapCircleAll(player.transform.position, 5f, player.Enemies);
        return arr;
    }
    public GameObject FindClosestEnemy(Player player)
    {
        float minDistance = Mathf.Infinity;
        Transform closest = null;
        Collider2D[] enemies = GetEnemiesInRange(player);
        float x;
        if (enemies.Length > 0)
        {
            foreach (Collider2D c in enemies)
            {
                if ((x = Vector2.Distance(player.transform.position, c.transform.position)) < minDistance)
                {
                    minDistance = x;
                    closest = c.transform;
                }
            }
        }
        return closest.gameObject;
    }
}
