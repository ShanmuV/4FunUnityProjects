using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] EnemyStateManager enemy;
    public void DoDamage(int damage)
    {
        enemy.TakeDamage(damage);
    }
    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DoDamage(5);
        }
    }
}
