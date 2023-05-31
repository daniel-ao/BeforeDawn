using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityRoyale
{
    public class ReloadSceneFirst : MonoBehaviour
    {
        private void Start()
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }
}
