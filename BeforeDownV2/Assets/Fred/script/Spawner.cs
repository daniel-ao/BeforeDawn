using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public float Health = 50; 
    public float SpawningRate = 0.2f;
    private float SpawningCountdown = 0f;
    public GameObject[] Units; //index : Blue -> [0:2], Red -> [3:5]
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimeToSpawn();
    }

    void spawner()
    {
        int indexer = 0;
        if (transform.tag == "Red")
        {
            indexer += 3;
        }
        Vector3 frontSpawn = new Vector3(-2, 0, 0);
        System.Random random = new System.Random();
        indexer += random.Next(0, 3);
        GameObject unit = Instantiate(Units[indexer], transform.position + frontSpawn, Quaternion.identity) as GameObject;
        if (indexer > 2)
        {
            unit.GetComponent<Transform>().tag = "Red";
        }
        else
        {
            unit.GetComponent<Transform>().tag = "Blue";
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
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

