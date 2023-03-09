using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class playerController : MonoBehaviour
{
    public LayerMask clickOn;
    public PlayerData playerdata;
    public LayerMask WhatIsEnemy;
    public Transform Enemy;
    private float SightRange, AttackRange;
    private RaycastHit hitInfo;
    private Ray ray;
    private NavMeshAgent Nav;
    private Animator animator;
    private Vector3 currentPosition;
    private bool alreadyAttack; 
    PhotonView view;
    

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        Nav = GetComponent<NavMeshAgent>();
        currentPosition = transform.position;
        Enemy = GameObject.Find("Aiknight").transform;
        alreadyAttack = false;
        view = GetComponent<PhotonView>();
    }
    // Update is called once per frame

    private void Update()
    {
        if (view.IsMine)
        {
             //  for letf click
                    bool click1 = Input.GetMouseButtonDown(1);
                    click(click1);
                    SightRange = Vector3.Distance(transform.position, Enemy.position);
                    AttackRange = Vector3.Distance(transform.position, Enemy.position);
            
                    if (AttackRange > playerdata.AttackRange && SightRange <= playerdata.SightRange)
                    {
                        Debug.Log("Chasing!");
                        ChasingEnnemy();
            
                    }
                    if (AttackRange <= playerdata.AttackRange && SightRange <= playerdata.SightRange)
                    {
                        Debug.Log("Attackiiiiinnnggg!");
                        Attacking();
                    }
        }
       
    }

    private void click(bool click1)
    {
        if (click1)
        {
            animator.SetBool("isRunning", true);
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, 100, clickOn))
            {
                Nav.SetDestination(hitInfo.point);
                
            }
        }

        else if (Nav.remainingDistance <= 0) 
        {
            animator.SetBool("isRunning", false);
        }
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
    private void ChasingEnnemy()
    {
        Nav.SetDestination(Enemy.position);
    }
    private void Attacking()
    {
        transform.LookAt(Enemy);
        AttackAnimation(alreadyAttack);
        if (!alreadyAttack)
        {
            alreadyAttack = true;
            Invoke(nameof(ResetAttack), 1);
        }
        
    }
    private void ResetAttack()
    {
        alreadyAttack = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, playerdata.SightRange);
        Gizmos.DrawWireSphere(transform.position, playerdata.AttackRange);
    }

}