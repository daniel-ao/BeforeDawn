using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class CameraController : MonoBehaviourPun
{
    [Header("Info")] public int id;
    public int joueur;
    public Player photonPlayer;
    
    public float panSpeed = 20f;
    public float ScrollSpeed = 200f;


    public float panBorderThickness = 10f;
    public float panLimit_Max_X;
    public float panLimit_Min_X;
    public float panLimit_Max_Z;
    public float panLimit_Min_Z;
    public float panLimit_Max_Y;
    public float panLimit_Min_Y;
    
    [PunRPC]
    public void Initialize (Player player)
    {
        id = player.ActorNumber;
        photonPlayer = player;
        //_GameManager.instance.players[id - 1] = this;
    
        // is this not our local player?
        if(!photonView.IsMine)
        {
            GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = transform.position;
        if (Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            Pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.y <= panBorderThickness)
        {
            Pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            Pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x <= panBorderThickness)
        {
            Pos.z += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Pos.y -= scroll * ScrollSpeed * Time.deltaTime;


        Pos.x = Mathf.Clamp(Pos.x, panLimit_Min_X, panLimit_Max_X);
        Pos.z = Mathf.Clamp(Pos.z, panLimit_Min_Z, panLimit_Max_Z);
        Pos.y = Mathf.Clamp(Pos.y, panLimit_Min_Y, panLimit_Max_Y);

        
        transform.position = Pos;
    }
}
