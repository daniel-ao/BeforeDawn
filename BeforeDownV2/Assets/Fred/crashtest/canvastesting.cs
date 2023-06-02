using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class canvastesting : MonoBehaviour
{
    private Canvas canvasReference;
    public GameObject healthBarPrefab;


    // Start is called before the first frame update
    void Start()
    {
        canvasReference = GameObject.FindObjectOfType<Canvas>();
    }

    void SpawnHealthBar(Vector3 position)
    {
        GameObject healthBar = Instantiate(healthBarPrefab, position, Quaternion.identity);
        healthBar.transform.SetParent(canvasReference.transform, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddGameObjectToCanvas(GameObject gameObjectToAdd)
    {
        if (gameObjectToAdd.transform.parent != canvasReference.transform)
        {
            gameObjectToAdd.transform.SetParent(canvasReference.transform, false);
        }
    }
}
