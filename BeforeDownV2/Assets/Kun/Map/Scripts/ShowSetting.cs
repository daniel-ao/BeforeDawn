using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSetting : MonoBehaviour
{
    public GameObject SETTingsPanel;
    private bool Showed = false;
     

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
       
        if(Showed)
        {
            Hidesetting();
            Showed = false;
        }

        else
        {
            Showsetting();
            Showed = true;
        }
    }

}
