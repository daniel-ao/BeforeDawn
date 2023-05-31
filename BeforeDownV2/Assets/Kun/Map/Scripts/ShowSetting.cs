using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSetting : MonoBehaviour
{
    public GameObject SETTingsPanel;
    public GameObject ExitPannel;
    public GameObject LosePannel;
    private bool Showed;


    public void ShowLosePannel()
    {
        LosePannel.SetActive(true);
    }

    public void ShowExit()
    {
        ExitPannel.SetActive(true);
    }

    public void HideExit()
    {
        ExitPannel.SetActive(false);
    }

    public void Showsetting()
    {
        SETTingsPanel.SetActive(true);
    }

    public void Hidesetting()
    {
        SETTingsPanel.SetActive(false);
    }

    public void ShowSettingOrNot()
    {
       
        if(SETTingsPanel.activeSelf)
        {
            Hidesetting();
            if(ExitPannel.activeSelf)
            {
                HideExit();
            }
        }

        else
        {
            Showsetting();
        }
    }

}
