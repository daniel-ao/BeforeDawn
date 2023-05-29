using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour

{

    public string sceneName;
    public int nb = 0;

    public void ChnageS()
    {
        SceneManager.LoadScene(sceneName,LoadSceneMode.Single);
    }


    public void ReloadSB()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1,LoadSceneMode.Single);
    }

    public void ReloadS()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
    }

    public void LoadNextS()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1,LoadSceneMode.Single);
    }

    public void LoadS()
    {
        SceneManager.LoadScene(nb,LoadSceneMode.Single);
    }

}
