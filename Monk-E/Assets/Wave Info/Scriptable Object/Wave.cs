using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Wave", menuName ="New Wave")]
public class Wave : ScriptableObject
{
    public int wave_number;
    public int fire_enemies;
    public int water_enemies;
}
