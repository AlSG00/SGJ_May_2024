using UnityEngine;

[CreateAssetMenu(fileName = "Game settings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private float _masterVolume;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _mouseSmoothing;

    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = Mathf.Clamp(value, 0, 1);
        }
    }
    public float MouseSensitivity
    {
        get => _mouseSensitivity;
        set
        {
            _mouseSensitivity = Mathf.Clamp(value, 0.1f, 6);
        }
    }
    public float MouseSmoothing
    {
        get => _mouseSmoothing;
        set
        {
            _mouseSmoothing = Mathf.Clamp(value, 0.01f, 0.3f);
        }
    }
}
