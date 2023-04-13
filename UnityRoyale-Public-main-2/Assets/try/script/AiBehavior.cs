using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace UnityRoyale
{
    public class AiBehavior : MonoBehaviour
    {
        public AiData data;

        public GameObject TowerB, TowerR;
        private Animator animator;
        private NavMeshAgent navAgent;
        
        private GameObject[] AllEnemy;
        private float Health;
        private float Damage;
        private float AttackRange;
        private float Speed;
        private bool Projectile;

        private void Awake()
        {
            Health = data.Health;
            Damage = data.damage;
            AttackRange = data.AttackRange;
            Speed = data.speed;
            Projectile = data.projectile;
        }

        // Start is called before the first frame update
        void Start()
        {
            navAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            if (transform.CompareTag("Red"))
            {
                AllEnemy = GameObject.FindGameObjectsWithTag("Blue");
            }
            else
            {
                AllEnemy = GameObject.FindGameObjectsWithTag("Red");
            }
        }

        // Update is called once per frame
        void Update()
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

            if (AllEnemy.Length == 0)
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
            else
            {
                target = FindClosest();
            }
            Chasing(target);
            Attacking(target);

        }

        private void Chasing(GameObject target)
        {
            animator.SetBool("IsMoving", true);
            navAgent.SetDestination(target.transform.position);
        }

        private void Attacking(GameObject target)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= data.AttackRange)
            {
                Debug.Log("atta");
                navAgent.Stop();
                animator.SetTrigger("Attack");
                target.GetComponent<TowerBehavior>().DamageTaken(Damage);
            }
            
        }

        private GameObject FindClosest()
        {
            GameObject closestHere = gameObject;
            float leastDistance = Mathf.Infinity;

            foreach (var enemy in AllEnemy)
            {

                float distanceHere = Vector3.Distance(transform.position, enemy.transform.position);

                if (distanceHere < leastDistance)
                {
                    leastDistance = distanceHere;
                    closestHere = enemy;
                }
            }

            return closestHere;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
        }
    }
    
}
