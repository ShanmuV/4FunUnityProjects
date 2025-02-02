using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NewEnemyAttacks : MonoBehaviour
{

    public void SwordAttack(NewEnemyStateManager enemy)
    {
        if(!enemy.isAttacking)
        {
            enemy.isAttacking = true;
            _SwordAttack(enemy);
        }
    }

    private async void _SwordAttack(NewEnemyStateManager enemy)
    {
        Debug.Log("Sword Attack!");
        await Task.Delay(1000);
        enemy.isAttacking = false;
    }
}
