using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderValue : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _sliderText;


    // Start is called before the first frame update
    void Start()
    {
        SoundManeger.Instance.ChangeMasterSound(_slider.value);
        _slider.onValueChanged.AddListener((v) =>
        {
            _sliderText.text = (v*100).ToString("0");
            SoundManeger.Instance.ChangeMasterSound(v);
        });
    }
}
