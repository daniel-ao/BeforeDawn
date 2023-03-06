using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
