using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMouseSensitivitySlider : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Slider _sensitivitySlider;

    public static event System.Action<float> SensitivityChanged;

    private void Start()
    {
        _sensitivitySlider.value = _gameSettings.MouseSensitivity;
        SensitivityChanged?.Invoke(_gameSettings.MouseSensitivity);
    }

    public void SetSensitivity()
    {
        _gameSettings.MouseSensitivity = _sensitivitySlider.value;
        SensitivityChanged?.Invoke(_sensitivitySlider.value);
    }
}
