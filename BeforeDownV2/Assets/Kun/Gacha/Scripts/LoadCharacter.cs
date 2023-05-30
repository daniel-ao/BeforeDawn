using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UnityRoyale
{
    public class LoadCharacter : MonoBehaviour
    {
        public GameObject[] charaterPrefabs;
        public Transform spawnPoint;
        public TMP_Text labe;


        void Start()
        {
            int selectedCharater = PlayerPrefs.GetInt("selectedCharacter");
            GameObject prefab = charaterPrefabs[selectedCharater];
            labe.text = prefab.name;
            
        }

    }
}
