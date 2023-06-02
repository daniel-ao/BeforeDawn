using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class _SceneManager : MonoBehaviourPun
{
    public RotaryTablePanel panel;
    // Update is called once per frame
    void Update()
    {
        if (panel.drawWinning)
            panel.photonView.RPC("Character", RpcTarget.All);
    }
}
