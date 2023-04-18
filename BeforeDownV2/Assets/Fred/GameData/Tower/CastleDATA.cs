using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Castle", menuName = "BeforeDown/Castle")]
public class CastleDATA : ScriptableObject
{
    public float Health = 15f;
    public float damage = 3f;
    public float AttackRange = 5f;
    public float fireRate = 1f;
    public float SpawningRate = 0.2f;
    public GameObject projectilePrefab;
}