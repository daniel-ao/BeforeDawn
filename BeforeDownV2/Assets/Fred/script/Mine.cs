using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;


public class Mine : MonoBehaviourPun
{
    float TimeBeforeDestroy = 40f;
    public float timer = 0f;
    public float GoldStockage = 750;
    private _GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("_GameManager").GetComponent<_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer >= TimeBeforeDestroy)
        {
            if (photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
                gameManager.SpawnMine();
            }
        }
    }

    public void TakeGold(GameObject miner)
    {
        System.Random random = new System.Random();
        float amout = random.Next(80, 120);
        if (miner.GetComponent<Miner>().Health > 0)
        {
            if (amout <= GoldStockage)
            {
                miner.GetComponent<Miner>().GoldStock += amout;
                GoldStockage -= amout;
            }
            else
            {
                miner.GetComponent<Miner>().GoldStock += GoldStockage;
                GoldStockage = 0;
            }
        }

    }
}
