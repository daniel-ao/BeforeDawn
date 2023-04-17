using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AI", menuName = "BeforeDown/AiData")]
public class AiData : ScriptableObject
{
    public GameObject Type;
    public float Health = 5.0f;
    public float speed = 3f;
    public float damage = 2f;
    public float SightRange = 6f;
    public float AttackRange = 1f;
    public float timeBetweenAttack = 1f;
    public bool isLongRange;
    public GameObject projectilePrefab;
}