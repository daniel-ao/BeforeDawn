using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [SerializeField] Text TimeText;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    float time;

    void FixedUpdate()
    {

        if (Win.activeSelf || Lose.activeSelf)
        {
            return;
        }

        else
        {
            time += Time.fixedDeltaTime;
            TimeText.text = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss\:ff");
            
        }
        
    }
}
