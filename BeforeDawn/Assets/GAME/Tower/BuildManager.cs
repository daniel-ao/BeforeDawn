using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject turretToBuild;
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one buildmanager in screen!");
            return;
        }
        instance = this;
    }
    public GameObject GetTurret()
    {
        return turretToBuild;
    }

    public GameObject TurretPrefab;
    void Start()
    {
        turretToBuild = TurretPrefab;
    }

}
