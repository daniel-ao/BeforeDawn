using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "GAME/HERO/Playerdata")]
public class PlayerData : ScriptableObject
{
    public float hp;
    public float atckDmg;
    public float SightRange;
    public float AttackRange;
    public float speedAtck;
}
