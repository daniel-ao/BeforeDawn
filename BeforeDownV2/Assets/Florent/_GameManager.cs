using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Random = UnityEngine.Random;

public class _GameManager : MonoBehaviourPun
{
    [Header("Players")]
    public string HeroPrefabLocation;
    public CameraController[] players;

    public Transform[] spawnPoints;
    private int playersInGame;
    public static _GameManager instance;
    void Awake ()
    {
        instance = this;
    }

    private void Start()
    {
        players = new CameraController[PhotonNetwork.PlayerList.Length];

        photonView.RPC("ImInGame", RpcTarget.AllBuffered);
    }
    
    [PunRPC]
    void ImInGame ()
    {
        playersInGame++;
        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
            photonView.RPC("SpawnTower",RpcTarget.All);
        }
            
    }
    
    [PunRPC]
    void SpawnTower()
    {
        GameObject Tower;
        if (PhotonNetwork.IsMasterClient)
        {
            Tower = PhotonNetwork.Instantiate("Barracks Tower Red", spawnPoints[4].position, Quaternion.identity);
        }
        else
        {
            Tower = PhotonNetwork.Instantiate("Barracks Tower Blue", spawnPoints[5].position, Quaternion.identity);

        }
    }
    
    [PunRPC]
    void SpawnPlayer ()
    {
        GameObject playerObj;
        GameObject HeroObj;
        if (PhotonNetwork.IsMasterClient)
        {
            playerObj = PhotonNetwork.Instantiate("Player1", spawnPoints[0].position, Quaternion.identity);
            HeroObj = PhotonNetwork.Instantiate("HeroKnight", spawnPoints[2].position, Quaternion.identity);

            playerObj.tag = "Red";
            HeroObj.tag = "Red";
        }

        else
        {
            playerObj = PhotonNetwork.Instantiate("Player2", spawnPoints[1].position, Quaternion.identity);
            HeroObj = PhotonNetwork.Instantiate("HeroKnight", spawnPoints[3].position, Quaternion.identity);
            
            var transformRotation = playerObj.transform.rotation;
            transformRotation.y = 180;
            HeroObj.transform.rotation = transformRotation;

            playerObj.tag = "Blue";
            HeroObj.tag = "Blue";
        }
        // initialize the player for all other players
        playerObj.GetComponent<CameraController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
