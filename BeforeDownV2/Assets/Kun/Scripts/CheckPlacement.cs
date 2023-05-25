using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    public GameObject Tower;
    private BuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Miner>(out Miner miner))
        {
            this.GetComponent<TowerBehavior>().CurrentGold += miner.GoldStock;
            miner.GoldStock = 0;
        }
        else if (other.gameObject.CompareTag("Red") || other.gameObject.CompareTag("Blue"))
        {
            _buildingManager.canPlace = false;
        }
    }
   

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Miner>(out Miner miner))
        {
            return;
        }
        else if (other.gameObject.CompareTag("Red") || other.gameObject.CompareTag("Blue")) 
        {
            _buildingManager.canPlace = true;
        }
    }
}

