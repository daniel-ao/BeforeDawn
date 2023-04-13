using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth = 50;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void takeDamage (float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
