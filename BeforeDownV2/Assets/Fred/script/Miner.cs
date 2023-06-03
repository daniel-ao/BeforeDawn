using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class Miner : MonoBehaviourPun
{
    Animator animator;
    public NavMeshAgent navAgent;
    GameObject target;
    private GameObject TowerB, TowerR;

    public float MaxHealth = 12f;
    public float Health = 12f;
    private float Speed = 3;
    private bool isAlive = true;
    public float GoldStock = 0f;
    float pickGoldRange = 2f;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //healthBar.SetMaxHealth(MaxHealth);
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = Speed;

        animator = GetComponent<Animator>();

        TowerR = GameObject.Find("Barracks Tower Red(Clone)");
        TowerB = GameObject.Find("Barracks Tower Blue(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        if (target == null)
        {
            return;
        }
        else
        {
            if (!target.CompareTag("Mine"))
            {
                Chasing(target);
                navAgent.isStopped = false;
            }
            else
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance <= pickGoldRange && GoldStock == 0f && isAlive)
                {
                    UnChasing();
                    navAgent.isStopped = true;
                    PickUpGold();
                }
                else
                {
                    Chasing(target);
                    navAgent.isStopped = false;
                }
            }
        }
    }

    void FindTarget()
    {
        if (GoldStock == 0f)
        {
            GameObject[] mines = GameObject.FindGameObjectsWithTag("Mine");

            float closestDistance = Mathf.Infinity;
            GameObject closestEnemy = null;
            foreach (GameObject mine in mines)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, mine.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = mine;
                }
            }
            if (closestEnemy != null)
            {
                target = closestEnemy;
            }
            else
            {
                target = null;
            }
        }
        else
        {
            if (transform.CompareTag("MinerRed"))
            {
                target = TowerR;
            }
            else
            {
                target = TowerB;
            }
            animator.SetBool("IsMoving", true);
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
    

    void PickUpGold()
    {
        if (target == null)
        {
            return;
        }

        Mine theMine = target.GetComponent<Mine>();
        if (theMine != null)
        {
            theMine.TakeGold(gameObject);
        }
    }

    public void TakeDamage(float amout)
    {
        Health -= amout;
        healthBar.SetHealth(Health);
        if (Health <= 0 && isAlive)
        {
            isAlive = false;
            navAgent.isStopped = true;
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
