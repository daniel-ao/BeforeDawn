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

        private GameObject TowerB, TowerR;
        private Animator animator;
        private NavMeshAgent navAgent;
        
        private GameObject[] AllEnemy;
        public float Health;
        private float Damage;
        private float AttackRange;
        private float SightRange;
        private float Speed;
        private bool Projectile;
        private float TimeAttack;
        private float Timer = 0f;
        private bool attacking = false;

        private void Awake()
        {
            Health = data.Health;
            Damage = data.damage;
            AttackRange = data.AttackRange;
            SightRange = data.SightRange;
            Speed = data.speed;
            Projectile = data.projectile;
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
            Debug.Log(navAgent.isStopped); 
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
            Debug.Log(target); 
            transform.LookAt(target.transform.position);
            Chasing(target);
            
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= AttackRange)
            {
                if (Timer <= 0f)
                {
                    Attacking(target);
                    Timer = 1f / TimeAttack;
                }
                Timer -= Time.deltaTime;
            }
        }

        private void Chasing(GameObject target)
        {
            navAgent.SetDestination(target.transform.position);
            Debug.Log(target.transform.position);
            Debug.Log(target);
            animator.SetBool("IsMoving", true);
        }

        private void Attacking(GameObject target)
        {
            Debug.Log("atta");
            if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
            {
                if (target.GetComponent<TowerBehavior>().Health>0)
                {
                    target.GetComponent<TowerBehavior>().DamageTaken(Damage);
                    animator.SetTrigger("Attack"); 
                }
                   
            }
            else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
            {
                if (target.GetComponent<AiBehavior>().Health>0)           
                {                                                            
                    target.GetComponent<AiBehavior>().DamageTaken(Damage);
                    animator.SetTrigger("Attack");                           
                }
            }
            else
            {
                return;
            }
            
                 
        }
        /**
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
        **/
        
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
        public void DamageTaken(float amout)
        {
            Health -= amout;
            if (Health <= 0)
            {
                animator.ResetTrigger("Attack");
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
    
}
