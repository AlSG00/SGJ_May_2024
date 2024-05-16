using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerVolumeController : MonoBehaviour
{
    private void OnEnable()
    {
        SetMasterVolumeSlider.VolumeChanged += SetVolume;
    }

    private void OnDisable()
    {
        SetMasterVolumeSlider.VolumeChanged -= SetVolume;
    }

    private void SetVolume(float volume)
    {
        Debug.Log("Listener Volume Setted");
        AudioListener.volume = volume;
    }
}
