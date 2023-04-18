using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinAndLose : MonoBehaviour
{
    public bool win = false;
    public bool lose = false;

    public GameObject WinPanel;
    public GameObject LosePanel;

    private void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }


    void Update()
    {
        if (win)
        {
            WinPanel.SetActive(true);
            WinPanel.transform.GetChild(WinPanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = "YOU WIN !";
        }

        if(lose)
        {
            LosePanel.SetActive(true);
            LosePanel.transform.GetChild(LosePanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = "YOU LOSE !";
        }
        
    }
}
