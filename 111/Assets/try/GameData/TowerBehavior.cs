using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Playables;

public class TowerBehavior : MonoBehaviour
{
    public CastleDATA castle;
    public PlayableDirector CastleCollapse;

    public float Health;
    private float Damage;
    private float AttackRange;
    private float fireCountdown = 0f;
    private Transform target;
    public Vector3 popo; //hauteur de la tower pour attaquer
    private bool NoHealth = false;

    private void Awake()
    {
        Health = castle.Health;
        Damage = castle.damage;
        AttackRange = castle.AttackRange;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NoHealth)
        {
            CastleCollapse.Play();
            StartCoroutine(WaitDestruction());
        }

        FindTarget();
        if (target != null)
        {
            if (fireCountdown <= 0f)
            {
                Fire();
                fireCountdown = 1f / castle.fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    public void TakeDamage(float amout)
    {
        Health -= amout;
        if (Health <= 0)
        {
            NoHealth = true;
        }
    }
    public IEnumerator WaitDestruction()
    {
        yield return new WaitForSeconds(5);
            
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