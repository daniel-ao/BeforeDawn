using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGame : MonoBehaviour
{
    public GameObject drawEndPanel;

    private void Start()
    {
        drawEndPanel.SetActive(false);
    }

    public void StartDraw(string CharacterName)
    {
        drawEndPanel.SetActive(true);
        drawEndPanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = CharacterName;
        //drawEndPanel.transform.GetChild(drawEndPanel.transform.childCount - 1).GetComponent<TextMeshProUGUI>().text = CharacterName;
    }
}
