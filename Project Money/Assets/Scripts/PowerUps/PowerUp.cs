using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp
{
    public abstract void Initialize(GameObject prefab);
    public abstract void Shoot(Player player);
    public abstract float GetShootDelay();
    public abstract void UpdateState(Player player);
}
