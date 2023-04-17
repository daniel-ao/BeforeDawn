using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;


    public class playerClickController : MonoBehaviour
    {
        public LayerMask clickOn;
        public HeroData playerdata;
        public Camera camera;
        public Vector3 popo;
        public float Health;
        
        private GameObject[] Enemy;
        private RaycastHit hitInfo;
        private Ray ray;
        private NavMeshAgent Nav;
        private Animator animator;
        private float Timer = 0f;
        private bool continueAttacking;

        // Start is called before the first frame update
        private void Awake()
        {
            Health = playerdata.Health;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            Nav = GetComponent<NavMeshAgent>();
            Enemy = null;
        }
        // Update is called once per frame

        private void Update()
        {
            GameObject target;
            bool attack;
            bool clickIn = Input.GetMouseButtonDown(1);
            click(clickIn);
            target = FindTarget();
            Debug.Log(target);
            if (target != null)
            {
                transform.LookAt(target.transform.position);
                ChasingEnnemy(target);
                
                float distance = Vector3.Distance(target.transform.position, transform.position);
                if (distance <= playerdata.AttackRange)
                {
                    Nav.isStopped = true;
                    if (Timer <= 0f)
                    {
                        Attacking(target);
                        Timer = 1f / playerdata.TimeAttack;
                    }

                    Timer -= Time.deltaTime;
                }
                else
                {
                    Nav.isStopped = false;
                }
            }
            
        }

        private void click(bool click1)
        {
            if (click1)
            {
                animator.SetBool("IsMoving", true);
                ray = camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    if (hitInfo.collider.CompareTag("Ground"))
                    {
                        Nav.SetDestination(hitInfo.point);
                    } 
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
        private void ShortRangeAttack(GameObject target)
        {
            if (target.gameObject.TryGetComponent<TowerBehavior>(out TowerBehavior enemyComponent))
            {
                if (target.GetComponent<TowerBehavior>().Health > 0)
                {
                    animator.SetTrigger("Attack");
                    target.GetComponent<TowerBehavior>().TakeDamage(playerdata.Damage);
                }
            }
            else if (target.gameObject.TryGetComponent<AiBehavior>(out AiBehavior enemyComponents))
            {
                if (target.GetComponent<AiBehavior>().Health > 0)
                {
                    animator.SetTrigger("Attack");
                    target.GetComponent<AiBehavior>().TakeDamage(playerdata.Damage);
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
            if (playerdata.isLongRange)
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
                Instantiate(playerdata.projectilePrefab, transform.position + popo, Quaternion.identity) as GameObject;
            Projectile script = projectile.GetComponent<Projectile>();
            script.target = target.transform;
            script.damage = playerdata.Damage;
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
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

            if (closestEnemy != null && closestDistance <= playerdata.SightRange)
            {
                target = closestEnemy;
            }
            else
            {
                target = null;
            }

            return target;
        }
        public Transform InteractionWithPlayer()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseHit());
            foreach (var hit in hits)
            {
                AiBehavior target = hit.transform.GetComponent<AiBehavior>();
                if (target == null)
                {
                    Debug.Log("pas la");
                }
                if (Input.GetMouseButtonDown(0))
                {
                    if (target.CompareTag("Red"))
                    {
                        return target.transform;
                    }
                }
            }

            return null;
        }

        private Ray GetMouseHit()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public void SelectedEnemy(AiBehavior target)
        {
            print("Enemy Selected");
        }

        public void TakeDamage(float amout)
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
            yield return new WaitForSeconds(1.5f);

            Destroy(gameObject);
        }
    }
