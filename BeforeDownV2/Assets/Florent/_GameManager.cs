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

    public Transform[] spawnPoints;
    private BuildingManager _buildingManager;
    private int playersInGame;
    public static _GameManager instance;

    private int selectedCharater1;
    private int selectedCharater2;
    
    private GameObject TowerR;
    private GameObject TowerB;
    void Awake ()
    {
        instance = this;
        _buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        selectedCharater1 = PlayerPrefs.GetInt("selectedCharacter1");
        selectedCharater2 = PlayerPrefs.GetInt("selectedCharacter2");
        photonView.RPC("ImInGame", RpcTarget.AllBuffered,selectedCharater1, selectedCharater2);
    }

    private void Start()
    {
        players = new CameraController[PhotonNetwork.PlayerList.Length];
    }
    
    [PunRPC]
    void MasterTag(GameObject gameObject, string tag)
    {
        gameObject.tag = tag;
    }

    [PunRPC]
    void ImInGame (int hero1, int hero2)
    {
        playersInGame++;
        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
            photonView.RPC("SpawnHero", RpcTarget.All, hero1, hero2);
            photonView.RPC("SpawnTower",RpcTarget.All);
            photonView.RPC("SpawnMiner", RpcTarget.AllBuffered);
            photonView.RPC("SpawnMine", RpcTarget.AllBuffered);
            
            _buildingManager.photonView.RPC("OwnTower",RpcTarget.AllBuffered, true);
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
    void SpawnHero(int hero1, int hero2)
    {
        GameObject HeroObj;
        GameObject prefab1 = charaterPrefabs[hero1];
        GameObject prefab2 = charaterPrefabs[hero2];
        if (PhotonNetwork.IsMasterClient)
        {
            HeroObj = PhotonNetwork.Instantiate(prefab1.name, spawnPoints[2].position, Quaternion.identity);
        }

        else
        {
            HeroObj = PhotonNetwork.Instantiate(prefab2.name, spawnPoints[3].position, Quaternion.identity);

            var transformRotation = new Quaternion();
            transformRotation.y = 180;
            HeroObj.transform.rotation = transformRotation;
        }
        
        HeroObj.GetComponent<playerClickController>().photonView.RPC("Initialize",RpcTarget.All,PhotonNetwork.IsMasterClient);
    }

    [PunRPC]
    void SpawnPlayer ()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject playerObj;
            playerObj = PhotonNetwork.Instantiate("Player1", spawnPoints[0].position, Quaternion.identity);
            
            playerObj.tag = "Red";
            playerObj.GetComponent<CameraController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
        }

        else
        {
            GameObject playerObj2;
            playerObj2 = PhotonNetwork.Instantiate("Player2", spawnPoints[1].position, Quaternion.identity);

            playerObj2.tag = "Blue";
            playerObj2.GetComponent<CameraController>().photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
        }
        // initialize the player for all other players
        
    }

    [PunRPC]
    void SpawnMiner()
    {
        
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject Miner;
            Miner = PhotonNetwork.Instantiate("miner", spawnPoints[6].position, Quaternion.identity);
            Miner.tag = "MinerRed";
        }
        else
        {
            GameObject Miner2;
            Miner2 = PhotonNetwork.Instantiate("miner", spawnPoints[7].position, Quaternion.identity);
            photonView.RPC("MasterTag",RpcTarget.MasterClient,Miner2, "MinerBlue");
        }
        
    }

    [PunRPC]
    void SpawnMine()
    {
        GameObject Mine;
        float x = Random.Range(-31, 31);
        float z = Random.Range(0, 31);
        Vector3 position = new Vector3();
        var positionPosition = position;
        if (PhotonNetwork.IsMasterClient)
        {
            positionPosition.x = TowerR.transform.position.x + x;
            positionPosition.z = TowerR.transform.position.z + z;
            position = positionPosition;
            Mine = PhotonNetwork.Instantiate("Mine", position, Quaternion.identity);
            Mine.tag = "Mine";
        }
        else
        {
            positionPosition.x = TowerB.transform.position.x + x;
            positionPosition.z = TowerB.transform.position.z - z;
            position = positionPosition;
            Mine = PhotonNetwork.Instantiate("Mine", position, Quaternion.identity);
            photonView.RPC("MasterTag",RpcTarget.MasterClient,Mine, "Mine");
        }
    }
}