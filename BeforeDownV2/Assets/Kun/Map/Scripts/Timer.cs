using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPun
{
    [SerializeField] Text TimeText;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    float time;

    void FixedUpdate()
    {

        if (Win.activeSelf || Lose.activeSelf || PhotonNetwork.PlayerList.Length != 2)
        {
            return;
        }

        else
        {
            time += Time.fixedDeltaTime;
            TimeText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss\:ff");
        }
        
    }
}
