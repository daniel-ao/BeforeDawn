using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class TurretControl : MonoBehaviour
{
    Transform _enemy;
    private NavMeshAgent navAgent;
    public TurretData turretData;
    private float nextShoot, distance;
    public Transform body, head, shooter; //body=support, head=character, shooter=weapon
    public GameObject _projected; //_projected=weapon (like arrow)
    // Start is called before the first frame update
    void Start()
    {
        if (navAgent == null)
        {
            navAgent = GetComponent<NavMeshAgent>();
        }

        // Si enemyData n'est pas vide, on va charger l'ennemi basé sur les infos qu'on a
        if (turretData != null)
        {
            LoadEnemy(turretData);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (navAgent == null)
        {
            return;
        }
        else
        {
            _enemy = GameObject.FindWithTag("enemy").transform;     // HAVE TO RELOAD THIS EVERY TIME TO KNOW IF WHERE IS AN ENNEMI WITHIN THE RANGE
            if (_enemy !=null)
            {
                distance = Vector3.Distance(_enemy.position, transform.position); //to know the position of oponnent
                if (distance <= turretData.maxRange)
                {
                    head.LookAt(_enemy);
                    if (Time.time >= nextShoot) //fireRate
                    {
                        nextShoot = Time.time + 1f / turretData.fireRate;
                        shoot();
                    }
                }
            }
            if (_enemy == null)
            {
                return;
            }
        } 
    }

    void shoot()
    {
        GameObject clone = Instantiate (_projected, shooter.position, head.rotation);
        clone.GetComponent<Rigidbody>().AddForce(head.forward * turretData.speedShoot);
        Destroy(clone, 3); //time to remove each bullet
    }

    /*
     * need to do damage
    */
    private void LoadEnemy(TurretData _data)
    {
        // Recupère les attribues de TurrentData et les attribuent au navmesh
        
    }
}
