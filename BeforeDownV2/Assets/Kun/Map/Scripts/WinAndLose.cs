using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinAndLose : MonoBehaviour
{
    private TowerBehavior tower;
    public bool Bleu = false;
    public bool Red = false;

    public bool win = false;
    public bool lose = false;

    public GameObject WinPanel;
    public GameObject LosePanel;

    private void Start()
    {
        tower = GetComponent<TowerBehavior>();
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }


    void Update()
    {
        if(Bleu && (tower.NoHealth || lose))
        {
            ShowLose(); 
        }

        if(win)
        {
            ShowWin();
        }
    }

    public void ShowWin()
    {
        WinPanel.SetActive(true);
        WinPanel.transform.GetChild(WinPanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = "YOU WIN !";
      
    }

    public void ShowLose()
    {
        LosePanel.SetActive(true);
        LosePanel.transform.GetChild(LosePanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = "YOU LOSE !";
    }
}
