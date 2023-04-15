using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RotaryTablePanel : MonoBehaviour
{
    // le button start
    public Button drawBtn;
    // l'élément parent des Récompenses
    public Transform rewardImgTran;
    // un halo lumineux
    public Transform HaloImgTransform;
    // les images des Récompenses
    private Transform[] rewardTransArr;

    // état par default 
    private bool isInitState;
    // gacha finie -- état terminé，le halo ne tourne plus
    private bool drawEnd;
    // gagner
    private bool drawWinning;

    // temps --> pour controller la vitesse de halo
    private float rewardTime = 0.8f;
    private float rewardTiming = 0;

    // l'index de image de récompense
    private int haloIndex = 0;
    // l'index de récompense
    private int rewardIndex = 0;

    // état entrain de pulling
    private bool isOnClickPlaying;

    void Start()
    {
        drawBtn.onClick.AddListener(OnClickDrawFun);
        rewardTransArr = new Transform[rewardImgTran.childCount];
        for (int i = 0; i < rewardImgTran.childCount; i++)
        {
            rewardTransArr[i] = rewardImgTran.GetChild(i);
           
        }

        // afficher le temps
        rewardTime = 0.6f;
        rewardTiming = 0;

        drawEnd = false;
        drawWinning = false;
        isOnClickPlaying = false;
    }


    void Update()
    {
        if (drawEnd) return;

        // montrer le recompense
        rewardTiming += Time.deltaTime;
        if (rewardTiming >= rewardTime)
        {
            rewardTiming = 0;
            haloIndex++;
            if (haloIndex >= rewardTransArr.Length)
            {
                haloIndex = 0;
            }
            SetHaloPos(haloIndex);
        }
    }

    // position de halo
    void SetHaloPos(int index)
    {
        HaloImgTransform.position = rewardTransArr[index].position;

        // gagner && l'index de image de récompense == l'index de récompense
        if (drawWinning && index == rewardIndex)
        {
            isOnClickPlaying = false;
            drawEnd = true;
            ///index est la index des images
            Debug.Log("Félicitation vous avez eu " + rewardTransArr[index].name + ".");
        }
    }

    // cliquer sur le statrt
    void OnClickDrawFun()
    {
        if (!isOnClickPlaying)
        {
            // avoir un ID rundom
            rewardIndex = Random.Range(0, rewardTransArr.Length);
            Debug.Log("c'est le récompense numéro " + (rewardIndex+1) + " que l'utilisateur va avoir.");
            isOnClickPlaying = true;
            drawEnd = false;
            drawWinning = false;
            StartCoroutine(StartDrawAni());
        }
    }

    /// commencer le pulling
    /// le halo tourne de moins en moins vite
    IEnumerator StartDrawAni()
    {
        rewardTime = 0.8f;
        // Accélération
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rewardTime -= 0.1f;
        }

        yield return new WaitForSeconds(2f);
        // Décélération
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            rewardTime += 0.1f;
        }

        yield return new WaitForSeconds(1f);
        drawWinning = true;
    }

}