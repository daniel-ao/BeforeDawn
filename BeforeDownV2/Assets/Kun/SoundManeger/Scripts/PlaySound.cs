using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _effectSound;
    [SerializeField] private Button button;
    [SerializeField] private Sprite OnButton;
    [SerializeField] private Sprite OffButton;

    private bool muted;

    public void PlayEffectSound()
    {
        SoundManeger.Instance.PlaySound(_effectSound);
    }

    public void UpdateButtonImg()
    {
        if(button.name == "Toggle_Music")
        {
            muted = SoundManeger.Instance.MusicMuted;
        }
        else
        {
            muted = SoundManeger.Instance.EffectMuted;
        }


        if (muted)
        {
            button.image.sprite = OffButton;
            
        }

        else
        {
            button.image.sprite = OnButton;
        }
    }

}



