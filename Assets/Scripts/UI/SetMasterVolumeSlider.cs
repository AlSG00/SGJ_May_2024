using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMasterVolumeSlider : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Slider _volumeSlider;

    public static event System.Action<float> VolumeChanged;

    private void Start()
    {
        _volumeSlider.value = _gameSettings.MasterVolume;
        VolumeChanged?.Invoke(_gameSettings.MasterVolume); 
    }

    public void SetVolume()
    {
        _gameSettings.MasterVolume = _volumeSlider.value;
        VolumeChanged?.Invoke(_volumeSlider.value);
    }
}
