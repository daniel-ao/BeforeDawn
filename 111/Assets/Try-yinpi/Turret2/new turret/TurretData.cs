using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Tower Defense/Turret")]
public class TurretData : ScriptableObject
{

    public float range = 10f;
    public float fireRate = 1f;
    public float damage = 10f;

    public GameObject projectilePrefab;
}