using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Aiscript : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatisGround, WhatisPlayer;
    public Transform PrincipalTower;
    public float timeBetAttack;
    private bool alreadyAttack;
    public float SightRange, AttackRange;
    private float AiSightRange, AiAttackRange;
    public Animator animator;
    public Vector3 vector;

    void Start()
    {
        alreadyAttack = false;
        player = GameObject.Find("HERO").transform;
        agent = GetComponent<NavMeshAgent>();
        PrincipalTower = GameObject.Find("PrincipaleTower").transform;
        animator = GameObject.Find("Animator").GetComponent<Animator>();
    }
    
    void Update()
    {
        AiSightRange = Vector3.Distance(transform.position, player.position);
        AiAttackRange = Vector3.Distance(transform.position, player.position);
        if (AiAttackRange > AttackRange && AiSightRange > SightRange)
        {
            AvancingToTower();
            Debug.Log("avance!");
        }

        if (AiAttackRange > AttackRange && AiSightRange <= SightRange)
        {
            Debug.Log("Chasing!");
            ChasingEnnemy();
            
        }
        if (AiAttackRange <= AttackRange && AiSightRange <= SightRange)
        {
            Debug.Log("Attackiiiiinnnggg!");
            Attacking();
        }
    }

    private void AvancingToTower()
    {
        agent.SetDestination(PrincipalTower.position - vector);
        transform.LookAt(PrincipalTower);
        Debug.Log(agent.transform.position);
    }
    private void ChasingEnnemy()
    {
        agent.SetDestination(player.position);
    }
    private void Attacking()
    {
        
        transform.LookAt(player);
        AttackAnimation(alreadyAttack);
        if (!alreadyAttack)
        {
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), timeBetAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    private void AttackAnimation(bool alreadyAttack)
    {
        if (alreadyAttack)
        {
            animator.SetBool("isAttacking", false);
        }
        else if (!alreadyAttack)
        {
            animator.SetBool("isAttacking", true);
        }
        {
        }
    }

}
