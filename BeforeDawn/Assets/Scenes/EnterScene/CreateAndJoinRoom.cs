using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRoom : MonoBehaviour
{
   public InputField createInput;
   public InputField joinInput;
   public void CreateRoom()
   {
      PhotonNetwork.CreateRoom(createInput.text);
   }

   public void JoinRoom()
   {
      PhotonNetwork.JoinRoom(joinInput.text);
   }

   public void OnJoinedRoom()
   {
      PhotonNetwork.LoadLevel("Game");
   }
}
