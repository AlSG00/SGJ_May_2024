using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMouseSmoothingSlider : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Slider _smoothingSlider;

    public static event System.Action<float> SmoothingChanged;

    private void Start()
    {
        _smoothingSlider.value = _gameSettings.MouseSmoothing;
        SmoothingChanged?.Invoke(_gameSettings.MouseSmoothing);
    }

    public void SetSmoothing()
    {
        _gameSettings.MouseSmoothing = _smoothingSlider.value;
        SmoothingChanged?.Invoke(_smoothingSlider.value);
    }
}
