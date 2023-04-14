using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


    public class playerClickController : MonoBehaviour
    {
        public LayerMask clickOn;
        public HeroData playerdata;
        public TagAttribute WhatIsEnemy;

        private GameObject[] Enemy;
        private float SightRange, AttackRange;
        private RaycastHit hitInfo;
        private Ray ray;
        private NavMeshAgent Nav;
        private Animator animator;
        private Vector3 currentPosition;
        private float Timer = 0f;

        // Start is called before the first frame update
        private void Start()
        {
            animator = GetComponent<Animator>();
            Nav = GetComponent<NavMeshAgent>();
            currentPosition = transform.position;
            Enemy = null;
        }
        // Update is called once per frame

        private void Update()
        {

            //  for letf click
            bool click1 = Input.GetMouseButtonDown(1);
            click(click1);
            GameObject target;
            target = FindTarget();
            if (target != null)
            {
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance <= playerdata.SightRange)
                {
                    ChasingEnnemy(target);
                }

                if (distance <= playerdata.AttackRange)
                {
                    if (Timer <= 0f)
                    {
                        Attacking(target);
                        Timer = 1f / playerdata.TimeAttack;
                    }
                    Timer -= Time.deltaTime;
                }
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
                    Debug.Log(hitInfo.point);
                }
            }

            else if (Nav.remainingDistance <= 0)
            {
                animator.SetBool("IsMoving", false);
            }
        }

        private void ChasingEnnemy(GameObject target)
        {
            Nav.SetDestination(target.transform.position);
        }

        private void Attacking(GameObject target)
        {
            transform.LookAt(target.transform);
            if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
            {
                if (target.GetComponent<TowerBehavior>().Health > 0)
                {
                    target.GetComponent<TowerBehavior>().TakeDamage(playerdata.Damage);
                    animator.SetTrigger("Attack");
                }

            }
            else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
            {
                if (target.GetComponent<AiBehavior>().Health > 0)
                {
                    target.GetComponent<AiBehavior>().TakeDamage(playerdata.Damage);
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                return;
            }


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
            playerdata.Health -= amout;
            if (playerdata.Health <= 0)
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
