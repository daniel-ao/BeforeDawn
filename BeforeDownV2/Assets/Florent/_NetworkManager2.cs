using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class _NetworkManager2 : MonoBehaviourPun
{
    private Player[] _players;
    public static _NetworkManager2 instance;
    
    void Awake ()
    {
        instance = this;
    }
    
    [PunRPC]
    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }

}
