using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TowerBehavior : MonoBehaviour
{
    public CastleDATA castle;

    public float Health;
    private float Damage;
    private float AttackRange;
    private float fireCountdown = 0f;
    private Transform target;
    public Vector3 popo; //hauteur de la tower pour attaquer
    public bool NoHealth = false;
    private bool Dying = true;

    private void Awake()
    {
        Health = castle.Health;
        AttackRange = castle.AttackRange;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!NoHealth)
        {
            FindTarget();
            if (target != null && !NoHealth)
            {
                if (fireCountdown <= 0f)
                {
                    Fire();
                    fireCountdown = 1f / castle.fireRate;
                }
                fireCountdown -= Time.deltaTime;
            }
        }
    }
    public void TakeDamage(float amout)
    {
        Health -= amout;
        if (Health <= 0)
        {
            NoHealth = true;
        }

        if (NoHealth && Dying)
        {
            StartCoroutine(WaitDestruction());
            Dying = false;
        }
    }
    public IEnumerator WaitDestruction()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
    void FindTarget()
    {
        GameObject[] enemies;
        if (transform.CompareTag("Red"))
        {
            enemies = GameObject.FindGameObjectsWithTag("Blue");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Red");
        }
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
        if (closestEnemy != null && closestDistance <= AttackRange)
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
        GameObject projectile = Instantiate(castle.projectilePrefab, transform.position + popo, Quaternion.identity) as GameObject;
        Projectile script = projectile.GetComponent<Projectile>();
        script.target = target;
        script.damage = castle.damage;
    }
}
