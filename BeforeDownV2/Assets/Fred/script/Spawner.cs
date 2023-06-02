using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Spawner : MonoBehaviourPun
{
    public float MaxHealth = 50;
    public float Health = 50; 
    public float SpawningRate = 0.2f;
    private float SpawningCountdown = 0f;
    private GameObject player;
    public HealthBar healthBar;

    public Transform SpawnPosition;
    public GameObject[] Units; //index : Blue -> [0:2], Red -> [3:5]

    private void Start()
    {
        healthBar.SetMaxHealth(MaxHealth);
    }

    void Update()
    {
        TimeToSpawn();
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
            GameObject unit = PhotonNetwork.Instantiate(Units[indexer].name, SpawnPosition.position, Quaternion.identity);
        }
    }
    void TimeToSpawn()
    {
        if (SpawningCountdown <= 0f)
        {
            spawner();
            SpawningCountdown = 1f / SpawningRate;
        }
        SpawningCountdown -= Time.deltaTime;
    }
    public void TakeDamage(float amout)
    {
        Health -= amout;
        healthBar.SetHealth(Health);
        if (Health <= 0)
        {
            if (photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}

