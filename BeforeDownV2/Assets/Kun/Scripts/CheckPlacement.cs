using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CheckPlacement : MonoBehaviourPun
{
    [SerializeField] private GameObject[] Towers;
    private BuildingManager _buildingManager;
    void Start()
    {
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Miner>(out Miner miner))
        {
            if (this.gameObject.name == "Barracks Tower Red(Clone)" || this.gameObject.name == "Barracks Tower Blue(Clone)")
            {
                
                GetComponent<TowerBehavior>().CurrentGold += miner.GoldStock;
                miner.GoldStock = 0;
                GetComponent<TowerBehavior>().ShowGoldPanel();
            }
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
    public GameObject SelectTower()
    {
        GameObject tower;
        if (PhotonNetwork.IsMasterClient)
        {
            tower = Towers[0];
            tower.tag = "Red";
        }
        else
        {
            tower = Towers[1];
            tower.tag = "Blue";
        }

        return tower;
    }
}

