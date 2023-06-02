using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
        if(transform.parent.tag == "Red" || transform.parent.tag == "MinerRed")
        {
            cam = GameObject.Find("Player1(Clone)");
        }
        else
        {
            cam = GameObject.Find("Player2(Clone)");
        }
    }
}
