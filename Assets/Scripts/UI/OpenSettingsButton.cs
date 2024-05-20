using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mouseOverAudio;
    [SerializeField] private AudioClip _mouseClickAudio;

    private void Start()
    {
        _settingsMenu.SetActive(false);
    }

    public void OnClick()
    {
        _audioSource.PlayOneShot(_mouseClickAudio);
        _settingsMenu.SetActive(!_settingsMenu.activeSelf);
    }
}
