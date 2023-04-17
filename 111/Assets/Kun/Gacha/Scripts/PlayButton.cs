using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _imag;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip compressClip, uncompressClip;
    [SerializeField] private AudioSource _audioSource;


    public void OnPointerDown(PointerEventData eventData)
    {
        _imag.sprite = _pressed;
        _audioSource.PlayOneShot(compressClip);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _imag.sprite = _default;
        _audioSource.PlayOneShot(uncompressClip);
    }

    public void IwasClicked()
    {
        Debug.Log("start to pulling ! ");
    }
}
