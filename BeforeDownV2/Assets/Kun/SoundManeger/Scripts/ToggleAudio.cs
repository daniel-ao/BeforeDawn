using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool _toggleMusic, _toggleEffect;

    public void Toggle()
    {
        if (_toggleEffect) SoundManeger.Instance.ToggleEffect();
        if (_toggleMusic) SoundManeger.Instance.ToggleMusic();
    }
}
