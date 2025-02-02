using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NewEnemyBaseState
{
    public abstract void EnterState(NewEnemyStateManager enemy);

    public abstract void UpdateState(NewEnemyStateManager enemy);

    public abstract void OnCollisionEnter(NewEnemyStateManager enemy, Collision collision);
}
