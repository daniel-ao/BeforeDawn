using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManeger : MonoBehaviour
{
    public static SoundManeger Instance;
    [SerializeField] private AudioSource _musicSource, _effectSource;
    public bool MusicMuted = false;
    public bool EffectMuted = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }

    public void ChangeMasterSound(float value)
    {
        AudioListener.volume = value;
    }


    public void ToggleEffect()
    {
        _effectSource.mute = !_effectSource.mute;
        EffectMuted = !EffectMuted;

    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
        MusicMuted = !MusicMuted;
    }

}
