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
        photonView.RPC("ImInGame", RpcTarget.AllBuffered, charaterPrefabs[selectedCharater1].name,
            charaterPrefabs[selectedCharater2].name);
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
    void ImInGame (string hero1, string hero2)
    {
        playersInGame++;
        if (PhotonNetwork.IsMasterClient && playersInGame == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("SpawnPlayer", RpcTarget.All);
            photonView.RPC("SpawnHero", RpcTarget.All, hero1,hero2);
            photonView.RPC("SpawnTower",RpcTarget.All);
            photonView.RPC("SpawnMiner", RpcTarget.AllBuffered);
            SpawnMine();
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
    public void SpawnHero(string hero1, string hero2)
    {
        GameObject HeroObj;
        if (PhotonNetwork.IsMasterClient)
        {
            HeroObj = PhotonNetwork.Instantiate(hero1, spawnPoints[2].position, Quaternion.identity);
        }

        else
        {
            HeroObj = PhotonNetwork.Instantiate(hero2, spawnPoints[3].position, Quaternion.identity);

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

    public void SpawnMine()
    {
        GameObject Mine;
        int nbmine = Random.Range(1, 3);
        for (int n = 0; n < nbmine; n++)
        {
            float x = Random.Range(-40, 40);
            float z = Random.Range(-20, 20);
            Vector3 position = new Vector3();
            var positionPosition = position;
            positionPosition.x = spawnPoints[8].position.x + x;
            positionPosition.z = spawnPoints[8].position.z + z;
            position = positionPosition;
            Mine = PhotonNetwork.Instantiate("Mine", position, Quaternion.identity);
            Mine.tag = "Mine";
        }
    }

    public void SpawnOneHero(string perso, string tag)
    {
        GameObject HeroObj;
        if (tag == "Red")
        {
            HeroObj = PhotonNetwork.Instantiate(perso, spawnPoints[2].position, Quaternion.identity);
        }
        else
        {
            HeroObj = PhotonNetwork.Instantiate(perso, spawnPoints[3].position, Quaternion.identity);

            var transformRotation = new Quaternion();
            transformRotation.y = 180;
            HeroObj.transform.rotation = transformRotation;
        }

        bool b = tag == "Red";
        HeroObj.GetComponent<playerClickController>().photonView.RPC("Initialize",RpcTarget.All,b);
        
    }
}