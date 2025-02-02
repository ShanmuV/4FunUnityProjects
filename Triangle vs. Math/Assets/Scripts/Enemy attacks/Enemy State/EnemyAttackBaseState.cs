
using UnityEngine;

public abstract class EnemyAttackBaseState
{
    public abstract void EnterPhase(MrMath enemy);
    public abstract void UpdatePhase(MrMath enemy);

}
