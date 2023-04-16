using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float damage;
    public Transform target;
    bool ok;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.LookAt(target);
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
            tower.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
