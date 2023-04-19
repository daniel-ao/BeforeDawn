using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mine : MonoBehaviour
{
    float TimeBeforeDestroy = 40f;
    public float timer = 0f;
    public float GoldStockage = 500;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBeforeDestroy)
        {
            Destroy(gameObject);
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
