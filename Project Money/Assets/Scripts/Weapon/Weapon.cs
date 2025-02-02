using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon
{
    public abstract void Initialize(GameObject projectile);
    public abstract float GetShootDelay();
    public abstract void Shoot(Player player);
}
