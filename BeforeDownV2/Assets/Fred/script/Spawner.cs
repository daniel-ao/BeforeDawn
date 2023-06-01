using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviourPun
{
    public float Health = 50; 
    public float SpawningRate = 0.2f;
    private float SpawningCountdown = 0f;
    private GameObject player;
    
    public Transform SpawnPosition;
    public GameObject[] Units; //index : Blue -> [0:2], Red -> [3:5]

    void Update()
    {
        photonView.RPC("TimeToSpawn",RpcTarget.AllViaServer);
    }
    void spawner()
    {
        if (photonView.IsMine)
        {
            int indexer = 0;
            if (gameObject.CompareTag("Red"))
            {
                indexer += 3;
            }

            System.Random random = new System.Random();
            indexer += random.Next(0, 3);
            Debug.Log(indexer);
            GameObject unit = PhotonNetwork.Instantiate(Units[indexer].name, SpawnPosition.position, Quaternion.identity);
        }
    }
    [PunRPC]
    void TimeToSpawn()
    {
        if (SpawningCountdown <= 0f)
        {
            spawner();
            SpawningCountdown = 1f / SpawningRate;
        }
        SpawningCountdown -= Time.deltaTime;
    }
    [PunRPC]
    public void TakeDamage(float amout)
    {
        Health -= amout;
        if (Health <= 0)
        {
            if (photonView.IsMine)
                Destroy(gameObject);
        }
    }
}

