using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float atkAmount = 1;
    public TurretControl turretControl;
    // Update is called once per frame
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
        {
            enemyComponent.takeDamage(atkAmount);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        atkAmount = turretControl.turretData.atckDmg;
    }
}
