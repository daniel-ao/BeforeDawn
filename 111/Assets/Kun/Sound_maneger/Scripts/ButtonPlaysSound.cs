using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPlaysSound : MonoBehaviour
{
    [SerializeField] private AudioClip _effectSound;

    public void PlayEffectSound()
    {
        SoundManeger.Instance.PlaySound(_effectSound);
    }
}
