using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public TurretData turretData;

    private Transform target;
    private float fireCountdown = 0f;



    void Update()
    {
        FindTarget();
        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Fire();
                fireCountdown = 1f / turretData.fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }
        if (closestEnemy != null && closestDistance <= turretData.range)
        {
            target = closestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Fire()
    {
        GameObject projectile = Instantiate(turretData.projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        Projectile script = projectile.GetComponent<Projectile>();
        script.target = target;
        script.damage = turretData.damage;
    }
}