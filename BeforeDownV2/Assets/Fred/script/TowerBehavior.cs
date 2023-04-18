using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class TowerBehavior : MonoBehaviour
{
    public CastleDATA castle;
    public PlayableDirector CastleCollapse;

    public float Health;
    private float Damage;
    private float AttackRange;
    private float fireCountdown = 0f;
    private float SpawningCountdown = 0f;
    private Transform target;
    public Vector3 popo; //hauteur de la tower pour attaquer
    private bool NoHealth = false;
    private bool Dying = true;
    public GameObject[] Units; //index : Blue -> [0:2], Red -> [3:5]

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
            TimeToSpawn();

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
            CastleCollapse.Play();
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


    void spawner()
    {
        int indexer = 0;
        if (transform.tag == "Red")
        {
            indexer += 3;
        }
        Vector3 frontSpawn = new Vector3(-2, 0, 0);
        System.Random random = new System.Random();
        indexer += random.Next(0, 3);
        GameObject unit = Instantiate(Units[indexer], transform.position + frontSpawn, Quaternion.identity) as GameObject;
        if (indexer > 2)
        {
            unit.GetComponent<Transform>().tag = "Red";
        }
        else
        {
            unit.GetComponent<Transform>().tag = "Blue";
        }
    }

    void TimeToSpawn()
    {
        if (SpawningCountdown <= 0f)
        {
            spawner();
            SpawningCountdown = 1f / castle.SpawningRate;
        }
        SpawningCountdown -= Time.deltaTime;
    }
}
