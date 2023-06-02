using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Projectile : MonoBehaviourPun
{
    public float speed = 10f;
    public float damage;
    public Transform target;
    bool ok;

    void Update()
    {
        if (target == null)
        {
            if (photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
            return;
        }

        photonView.transform.LookAt(target);
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        if (target == null)
        {
            return;
        }

        AiBehavior enemy = target.GetComponent<AiBehavior>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        TowerBehavior tower = target.GetComponent<TowerBehavior>();
        if (tower != null)
        {
            if (!tower.NoHealth)
                tower.TakeDamage(damage);
        }

        playerClickController player = target.GetComponent<playerClickController>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Spawner spawn = target.GetComponent<Spawner>();
        if (spawn != null)
        {
            spawn.TakeDamage(damage);
        }
        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
    }
}

