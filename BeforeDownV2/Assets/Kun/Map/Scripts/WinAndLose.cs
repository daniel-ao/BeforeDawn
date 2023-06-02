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
        Debug.Log("enter on script WINADNLOSE");
        if (!master)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("!masterIf");
                ShowWin();
            }

                
        
            else
            {
                Debug.Log("!masterElse");
                ShowLose();
            }
            
        }
        else
        {
            Debug.Log("master");
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("showLose");
                ShowLose();
            }
                
            else
            {
                Debug.Log("showWin");
                ShowWin();
            }
        }
    }

    public void ShowWin()
    {
        WinPanel.SetActive(true);
      
    }

    public void ShowLose()
    {
        LosePanel.SetActive(true);
    }
}
