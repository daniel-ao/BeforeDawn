using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Principal_turret : MonoBehaviour
{
    private Transform ennemy;

    public float range = 15f;

    void draw()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
