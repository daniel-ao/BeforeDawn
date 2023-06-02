using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Castle", menuName = "BeforeDown/Castle")]
public class CastleDATA : ScriptableObject
{
    public float Health = 15f;
    public float damage = 3f;
    public float AttackRange = 5f;
    public float CurrentGold = 100f;
    public float Price = 0f;
    public float fireRate = 1f;
    public Vector3 popo; //hauteur l'archer/soso pour attaquer
    public GameObject projectilePrefab;
}