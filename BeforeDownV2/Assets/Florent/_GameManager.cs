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
    public CameraController[] players;

    public GameObject[] charaterPrefabs;
    private GameObject[] Hero;

    public Transform[] spawnPoints;
    private int playersInGame;
    public static _GameManager instance;

    private int selectedCharater1;
    private int selectedCharater2;
    
    private GameObject TowerR;
    private GameObject TowerB;
    void Awake ()
    {
        instance = this;
    }

    private void Start()
    {
        Hero = new GameObject[PhotonNetwork.PlayerList.Length];
        players = new CameraController[PhotonNetwork.PlayerList.Length];
        selectedCharater1 = PlayerPrefs.GetInt("selectedCharacter1");
        selectedCharater2 = PlayerPrefs.GetInt("selectedCharacter2");
        photonView.RPC("ImInGame", RpcTarget.AllBuffered,selectedCharater1, selectedCharater2);
    }

    [PunRPC]
    void ImInGame (int hero1, int hero2)
    {
        playersInGame++;
        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All, hero1, hero2);
            photonView.RPC("SpawnTower",RpcTarget.All);
        }
            
    }
    
    [PunRPC]
    void SpawnTower()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            TowerR = PhotonNetwork.Instantiate("Barracks Tower Red", spawnPoints[4].position, Quaternion.identity);
        }
        else
        {
            TowerB = PhotonNetwork.Instantiate("Barracks Tower Blue", spawnPoints[5].position, Quaternion.identity);

        }
    }
    
    [PunRPC]
    void SpawnPlayer (int hero1, int hero2)
    {
        GameObject playerObj;
        GameObject HeroObj;
        GameObject prefab1 = charaterPrefabs[hero1];
        GameObject prefab2 = charaterPrefabs[hero2];
        if (PhotonNetwork.IsMasterClient)
        {
            playerObj = PhotonNetwork.Instantiate("Player1", spawnPoints[0].position, Quaternion.identity);
            HeroObj = PhotonNetwork.Instantiate(prefab1.name, spawnPoints[2].position, Quaternion.identity);
            Hero[0] = HeroObj;
            
            playerObj.tag = "Red";
            HeroObj.tag = "Red";
        }

        else
        {
            playerObj = PhotonNetwork.Instantiate("Player2", spawnPoints[1].position, Quaternion.identity);
            HeroObj = PhotonNetwork.Instantiate(prefab2.name, spawnPoints[3].position, Quaternion.identity);
            Hero[1] = HeroObj;
            
            var transformRotation = playerObj.transform.rotation;
            transformRotation.y = 180;
            HeroObj.transform.rotation = transformRotation;

            playerObj.tag = "Blue";
            HeroObj.tag = "Blue";
        }
        // initialize the player for all other players
        playerObj.GetComponent<CameraController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    void SpawnMine()
    {
        
    }
}