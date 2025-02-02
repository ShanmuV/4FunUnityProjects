using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireEnemyBase
{
    public abstract void EnterState(FireEnemyManager enemy);

    public abstract void UpdateState(FireEnemyManager enemy);

    public abstract void OnCollsionEnter(FireEnemyManager enemy);
}
