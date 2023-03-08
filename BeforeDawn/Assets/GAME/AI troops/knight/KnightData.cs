using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnightData", menuName = "TheGame/KnightData")]
public class KnightData : ScriptableObject
{
    public float hp;
    public float atckDmg;
    public float SightRange;
    public float AttackRange;
    public float speedAtck;
}
