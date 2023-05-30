using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class StopGame : MonoBehaviour
    {
        public GameObject GameFinishedPannel;
        public AudioSource SoundM;

        private void Update()
        {
            if (GameFinishedPannel.activeSelf)
            {
                Time.timeScale = 0;
                SoundM.Stop();

            }
        }
    }
}
