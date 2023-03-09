using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "turretData", menuName = "My_game/turrent_data")]
public class TurretData : ScriptableObject
{
    public float hp;
    public float atckDmg;
    public float maxRange;
    public float speedShoot;
    public float fireRate;
}