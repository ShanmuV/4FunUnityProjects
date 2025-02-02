using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase 
{
    public abstract void Shoot(PlayerAttackManager attack);

    public abstract void UpdateTimer(PlayerAttackManager attack);

}
