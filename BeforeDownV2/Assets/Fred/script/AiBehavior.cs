using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class AiBehavior : MonoBehaviour
{
    public AiData data;

    private GameObject TowerB, TowerR;
    private Animator animator;
    public NavMeshAgent navAgent;

    private GameObject[] AllEnemy;
    public float Health;
    private float Damage;
    private float AttackRange;
    private float SightRange;
    private float Speed;
    private bool IsLongRange;
    public Vector3 popo; //hauteur l'archer/soso pour attaquer
    private float TimeAttack;
    private float Timer = 0f;
    private bool isAlive = true;

    private void Awake()
    {
        Health = data.Health;
        Damage = data.damage;
        AttackRange = data.AttackRange;
        SightRange = data.SightRange;
        Speed = data.speed;
        IsLongRange = data.isLongRange;
        TimeAttack = data.timeBetweenAttack;
    }

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        TowerR = GameObject.Find("Barracks Tower Red");
        TowerB = GameObject.Find("Barracks Tower Blue");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target;
        target = FindTarget();
        if (target is null)
        {
            if (CompareTag("Blue"))
            {
                target = TowerR;
            }
            else
            {
                target = TowerB;
            }
        }
        if (target != null)
        {
            transform.LookAt(target.transform.position);

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= AttackRange)
            {
                UnChasing();
                navAgent.isStopped = true;
                if (Timer <= 0f)
                {
                    Attacking(target);
                    Timer = 1f / TimeAttack;
                }

                Timer -= Time.deltaTime;
            }
            else
            {
                Chasing(target);
                navAgent.isStopped = false;
            }
        }
    }

    private void Chasing(GameObject target)
    {
        navAgent.SetDestination(target.transform.position);
        animator.SetBool("IsMoving", true);
    }

    private void UnChasing()
    {
        animator.SetBool("IsMoving", false);
    }

    private void ShortRangeAttack(GameObject target)
    {

        if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
        {
            if (target.GetComponent<TowerBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<TowerBehavior>().TakeDamage(Damage);
            }

        }
        else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
        {
            if (target.GetComponent<AiBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                target.GetComponent<AiBehavior>().TakeDamage(Damage);
            }
        }
        else
        {
            return;
        }
    }

    private void LongRangeAttack(GameObject target)
    {
        if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
        {
            if (target.GetComponent<TowerBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                Fire(target);
            }

        }
        else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
        {
            if (target.GetComponent<AiBehavior>().Health > 0)
            {
                animator.SetTrigger("Attack");
                Fire(target);
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
            Instantiate(data.projectilePrefab, transform.position + popo, Quaternion.identity) as GameObject;
        Projectile script = projectile.GetComponent<Projectile>();
        script.target = target.transform;
        script.damage = Damage;
    }


    private GameObject FindTarget()
    {
        GameObject target;
        if (transform.CompareTag("Red"))
        {
            AllEnemy = GameObject.FindGameObjectsWithTag("Blue");
        }
        else
        {
            AllEnemy = GameObject.FindGameObjectsWithTag("Red");
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        foreach (GameObject enemy in AllEnemy)
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
    public void TakeDamage(float amout)
    {
        Health -= amout;
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            animator.SetTrigger("IsDead");
            StartCoroutine(WaitDie());
        }
    }

    public IEnumerator WaitDie()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}