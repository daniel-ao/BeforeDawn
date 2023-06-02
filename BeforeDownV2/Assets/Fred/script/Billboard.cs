using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Billboard : MonoBehaviour
{
    public GameObject cam;

    private void Start()
    {
        found();
    }

    void LateUpdate()
    {
        transform.LookAt(cam.transform.position);
    }
    void found()
    {
        Debug.Log(this.transform.parent.tag);
        if(this.transform.parent.tag == "Red")
        {
            cam = GameObject.Find("Player1(Clone)");
        }
        else
        {
            cam = GameObject.Find("Player2(Clone)");
        }
    }
}