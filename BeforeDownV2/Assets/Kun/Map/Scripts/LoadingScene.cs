using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadPanel;
    public Slider Bar;
    float times = 0.0f;


    public void LoadS(int sceneId)
    {
        LoadPanel.SetActive(true);

        StartCoroutine(LoadSceneAsync(sceneId));
    }

    

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        operation.allowSceneActivation = false;

        

            while (!operation.isDone && times < 5)
        {
            times += Time.fixedDeltaTime;
            float pregressValue = Mathf.Clamp01(operation.progress / 0.9f);
            Bar.value = pregressValue;
            yield return null;
        }

        operation.allowSceneActivation = true;

    }
    
    public void DisconnectPlayer()
    {
        Destroy(networkManager.instance.gameObject);
        StartCoroutine(DisconnectedAndLoad());
    }

    IEnumerator DisconnectedAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneBuildIndex: 0,LoadSceneMode.Single);
    }

    public void StartButton()
    {
        _NetworkManager2.instance.photonView.RPC("ChangeScene", RpcTarget.All,"Fred/scene/MapScene");
    }

    //IEnumerator LoadSceneAsync(int sceneId)
    //{
    //    LoadPanel.SetActive(true);
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
    //    operation.allowSceneActivation = false;
    //    yield return operation;
    //}

    //float timer = 0;

    //private void Update()
    //{
    //    if (LoadPanel.activeSelf && timer<5)
    //    {
    //        float progressValue = operation.progress / 0.9f;
    //        BarFill.fillAmount = progressValue;
    //        timer += Time.deltaTime;
    //    }

    //    else
    //    {
    //        operation.allowSceneActivation = true;
    //    }

    //}
}
