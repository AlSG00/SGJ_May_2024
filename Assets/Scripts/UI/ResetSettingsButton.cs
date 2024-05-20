using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSettingsButton : MonoBehaviour
{
    [SerializeField] private float _defaultVolume;
    [SerializeField] private float _defaultSensitivity;
    [SerializeField] private float _defaultSmoothing;

    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Slider _sensitivitySlider;
    [SerializeField] private Slider _smoothingSlider;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mouseOverAudio;
    [SerializeField] private AudioClip _mouseClickAudio;

    public void OnClick()
    {
        _audioSource.PlayOneShot(_mouseClickAudio);
        _volumeSlider.value = _defaultVolume;
        _sensitivitySlider.value = _defaultSensitivity;
        _smoothingSlider.value = _defaultSmoothing;
    }
}
