using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSetting : MonoBehaviour
{
    public GameObject SETTingsPanel;
     

    public void Showsetting()
    {
        SETTingsPanel.SetActive(true);
    }

    public void Hidesetting()
    {
        SETTingsPanel.SetActive(false);
    }


}
