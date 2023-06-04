using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Photon.Pun;
using UnityEngine.Serialization;

public class TowerBehavior : MonoBehaviourPun
{
    public CastleDATA castle;

    public float MaxHealth;
    public float Health;
    public float CurrentGold = 500f;
    private float AttackRange;
    private float fireCountdown = 0f;
    private Transform target;
    public Vector3 popo; //hauteur de la tower pour attaquer
    public bool NoHealth = false;
    private bool Dying = true;

    public GameObject TowerR;
    public GameObject TowerB;
    private WinAndLose winAndLose;
    
    public HealthBar healthBar;


    private void Awake()
    {
        MaxHealth = castle.Health;
        this.Health = MaxHealth;
        AttackRange = castle.AttackRange;
        popo = castle.popo;

        if (photonView.IsMine)
            healthBar.SetMaxHealth(MaxHealth);
        else
        {
            healthBar.gameObject.SetActive(false);
        }

    }

    private void Start()
    {
        
        if ( gameObject == TowerR || gameObject == TowerB)
            winAndLose = GameObject.Find("_GameManager").GetComponent<WinAndLose>();
    }

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
        healthBar.SetHealth(Health);
        if (Health <= 0)
        {
            NoHealth = true;
        }

        if (NoHealth && Dying)
        {
            StartCoroutine(WaitDestruction());
            Dying = false;
            if (gameObject == TowerR || gameObject == TowerB)
            {
                winAndLose.photonView.RPC("GameEnd",RpcTarget.All, PhotonNetwork.IsMasterClient);
            }
        }
    }
    public IEnumerator WaitDestruction()
    {
        yield return new WaitForSeconds(1.0f);
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
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
            if (distanceToEnemy < closestDistance && enemy.name != "Player1(Clone)" && enemy.name != "Player2(Clone)")
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
        GameObject projectile = PhotonNetwork.Instantiate(castle.projectilePrefab.name, transform.position + popo, Quaternion.identity);
        Projectile script = projectile.GetComponent<Projectile>();
        script.target = target;
        script.damage = castle.damage;
    }
}

