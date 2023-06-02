using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Photon.Realtime;

public class playerClickController : MonoBehaviourPun
{
    [Header("Info")] public int id;

    public LayerMask clickOn;
    public HeroData playerdata;
    public TagAttribute WhatIsEnemy;
    public float MaxHealth;
    public float Health;


    private GameObject[] Enemy;
    private float SightRange, AttackRange, TimeAttack, Damage;

    private RaycastHit hitInfo;
    private Ray ray;
    private NavMeshAgent Nav;
    private Animator animator;
    private bool IsLongRange;
    private float Timer = 0f;
    private bool isAlive = true;
    private bool isMovable = false;
    public Vector3 popo;


    public HealthBar healthBar;

    private PhotonView view;

    private void Awake()
    {

        MaxHealth = playerdata.Health;
        Health = playerdata.Health;
        SightRange = playerdata.SightRange;
        AttackRange = playerdata.AttackRange;
        TimeAttack = playerdata.TimeAttack;
        Damage = playerdata.Damage;
        popo = playerdata.popo;
        IsLongRange = playerdata.IsLongRange;

        healthBar.SetMaxHealth(MaxHealth);

    }

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent>();
        Enemy = null;

        view = GetComponent<PhotonView>();
    }
    private void Update()
    {
        bool click1 = Input.GetMouseButtonDown(1);
        click(click1);
        GameObject target;
        target = FindTarget();
        if (target != null && isAlive && isMovable)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= SightRange && distance > AttackRange)
            {
                ChasingEnnemy(target);
            }

            else if (distance <= AttackRange)
            {
                Nav.isStopped = true;
                if (Timer <= 0f)
                {
                    Attacking(target);
                    Timer = 1f / TimeAttack;
                }
                Timer -= Time.deltaTime;
            }
        }
        else
        {
            Nav.isStopped = false;
        }
    }

    private void click(bool click1)
    {
        if (click1)
        {
            isMovable = false;
            animator.SetBool("IsMoving", true);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, clickOn))
            {
                Nav.SetDestination(hitInfo.point);
            }
        }

        else if (Nav.remainingDistance <= 0)
        {
            animator.SetBool("IsMoving", false);
            isMovable = true;
        }
    }

    private void ChasingEnnemy(GameObject target)
    {
        animator.SetBool("IsMoving", true);
        Nav.SetDestination(target.transform.position);
    }

    private void UnChasing()
    {
        animator.SetBool("IsMoving", false);
    }

    private void ShortRangeAttack(GameObject target)
    {

        if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent1))
        {
            if (target.GetComponent<TowerBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<TowerBehavior>().photonView.RPC("TakeDamage", RpcTarget.AllViaServer, Damage);
            }

        }
        else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponent2))
        {
            if (target.GetComponent<AiBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<AiBehavior>().photonView.RPC("TakeDamage", RpcTarget.AllViaServer, Damage);
            }
        }
        else if (target.gameObject.TryGetComponent<Miner>(out Miner enemyComponent3))
        {
            if (target.GetComponent<Miner>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<Miner>().TakeDamage(Damage);
            }
        }
        else if (target.gameObject.TryGetComponent<Spawner>(out Spawner enemyComponent4))
        {
            if (target.GetComponent<Spawner>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<Spawner>().TakeDamage(Damage);
            }
        }
        else if (target.gameObject.TryGetComponent<playerClickController>(out playerClickController enemyComponent5))
        {
            if (target.GetComponent<playerClickController>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<playerClickController>().TakeDamage(Damage);
            }
        }
        else
        {
            return;
        }
    }

    private void LongRangeAttack(GameObject target)
    {
        if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent1))
        {
            if (target.GetComponent<TowerBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                Fire(target);
            }

        }
        else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponent2))
        {
            if (target.GetComponent<AiBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                Fire(target);
            }
        }
        else if (target.gameObject.TryGetComponent<Miner>(out Miner enemyComponent3))
        {
            if (target.GetComponent<Miner>().Health > 0)
            {
                animator.SetTrigger("Attack");
                Fire(target);
            }

        }
        else if (target.gameObject.TryGetComponent<Spawner>(out Spawner enemyComponent4))
        {
            if (target.GetComponent<Spawner>().Health > 0)
            {

                animator.SetTrigger("Attack");
                target.GetComponent<Spawner>().TakeDamage(Damage);
            }
        }
        else if (target.gameObject.TryGetComponent<playerClickController>(out playerClickController enemyComponent5))
        {
            if (target.GetComponent<playerClickController>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<playerClickController>().TakeDamage(Damage);
            }
        }
        else
        {
            return;
        }
    }

    private void Attacking(GameObject target)
    {
        if (IsLongRange)
        {
            LongRangeAttack(target);
        }
        else
        {
            ShortRangeAttack(target);
        }
    }

    void Fire(GameObject target)
    {
        GameObject projectile =
            PhotonNetwork.Instantiate(playerdata.projectilePrefab.name, transform.position + popo, Quaternion.identity) as GameObject;
        Projectile script = projectile.GetComponent<Projectile>();
        script.target = target.transform;
        script.damage = Damage;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerdata.SightRange);
        Gizmos.DrawWireSphere(transform.position, playerdata.AttackRange);
    }

    private GameObject FindTarget()
    {
        GameObject target;
        if (transform.CompareTag("Red"))
        {
            Enemy = GameObject.FindGameObjectsWithTag("Blue");
        }
        else
        {
            Enemy = GameObject.FindGameObjectsWithTag("Red");
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in Enemy)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null && closestDistance <= SightRange)
        {
            target = closestEnemy;
        }
        else
        {
            target = null;
        }

        return target;
    }

    public void TakeDamage(float amout)
    {
        Health -= amout;
        healthBar.SetHealth(Health);
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            Nav.isStopped = true;
            animator.SetTrigger("IsDead");
            StartCoroutine(WaitDie());
        }
    }

    public IEnumerator WaitDie()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
