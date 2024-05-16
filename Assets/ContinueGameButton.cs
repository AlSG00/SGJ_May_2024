using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mouseOverAudio;
    [SerializeField] private AudioClip _mouseClickAudio;
    public void OnClick()
    {
        _audioSource.PlayOneShot(_mouseClickAudio);
        _pauseMenu.Hide();
    }
}
