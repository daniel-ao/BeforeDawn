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
    public float Health;
    

    private GameObject[] Enemy;
    private float SightRange, AttackRange,TimeAttack, Damage;
    private RaycastHit hitInfo;
    private Ray ray;
    private NavMeshAgent Nav;
    private Animator animator;
    private float Timer = 0f;
    public bool isAlive = true;
    private PhotonView view;

    private void Awake()
    {
        Health = playerdata.Health;
        SightRange = playerdata.SightRange;
        AttackRange = playerdata.AttackRange;
        TimeAttack = playerdata.TimeAttack;
        Damage = playerdata.Damage;
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
        if (target != null && isAlive)
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

    private void Attacking(GameObject target)
    {
        transform.LookAt(target.transform);
        if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
        {
            if (target.GetComponent<TowerBehavior>().Health > 0)
            {
                target.GetComponent<TowerBehavior>().TakeDamage(Damage);
                animator.SetTrigger("Attack");
            }

        }
        else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
        {
            if (target.GetComponent<AiBehavior>().Health > 0)
            {
                target.GetComponent<AiBehavior>().TakeDamage(Damage);
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            return;
        }


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
        yield return new WaitForSeconds(1.5f);
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}
