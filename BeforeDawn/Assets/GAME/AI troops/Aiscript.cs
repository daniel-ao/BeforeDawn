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
    private float AiSightRange, AiAttackRange, AiAttackRangeTower;
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
        AiAttackRangeTower = Vector3.Distance(transform.position, PrincipalTower.position);
        if (AiAttackRange > AttackRange && AiSightRange > SightRange)
        {
            AvancingToTower(AiAttackRangeTower <= AttackRange);
        }

        if (AiAttackRange > AttackRange && AiSightRange <= SightRange)
        {
            ChasingEnnemy();
            
        }
        if (AiAttackRange <= AttackRange && AiSightRange <= SightRange)
        {
            Attacking(player);

        }
    }

    private void AvancingToTower(bool attacking)
    {
        agent.SetDestination(PrincipalTower.position - vector);
        if (attacking)
        {
            Attacking(PrincipalTower);
        }
        transform.LookAt(PrincipalTower);
    }
    private void ChasingEnnemy()
    {
        agent.SetDestination(player.position);
    }
    private void Attacking(Transform gameobject)
    {
        
        transform.LookAt(gameobject);
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
    }


}
