using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class WinAndLose : MonoBehaviourPun
{
    public GameObject TowerB;
    public GameObject TowerR;
    
    public bool win = false;
    public bool lose = false;

    public GameObject WinPanel;
    public GameObject LosePanel;

    private void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }


    [PunRPC]
    public void GameEnd(bool master)
    {
        if (!master)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ShowLose();
            }

                
        
            else
            {
                ShowWin();
            }
            
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                ShowWin();
            }
                
            else
            {
                ShowLose();
            }
        }
    }

    public void ShowWin()
    {
        WinPanel.SetActive(true);
        LosePanel.SetActive(false);
    }

    public void ShowLose()
    {
        LosePanel.SetActive(true);
    }
}
