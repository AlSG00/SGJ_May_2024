using UnityEngine;

public class Lightswitcher : InteractableItem
{
    [SerializeField] private bool _isActive;
    [SerializeField] private Light[] _lightArray;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Transform _switcher;

    private void Awake()
    {
        SetState(_isActive);
    }

    public override void Interact()
    {
        SwitchLight();
    }

    private void SwitchLight()
    {
        _isActive = !_isActive;

        foreach (var light in _lightArray)
        {
            light.enabled = _isActive;
        }

        SetSwitcherRotation();
        PlaySwitcherAudio();
    }

    private void SetSwitcherRotation()
    {
        float targetSwitcherAngle = _isActive ? 45 : -45;
        _switcher.localRotation = Quaternion.Euler(
            _switcher.localRotation.x,
            _switcher.localRotation.y,
            targetSwitcherAngle
            );
    }

    private void PlaySwitcherAudio()
    {
        _audioSource.pitch = Random.Range(0.98f, 1.02f);
        _audioSource.PlayOneShot(_audioClip);
    }

    private void SetState(bool setActive)
    {
        foreach (var light in _lightArray)
        {
            light.enabled = _isActive;
        }

        SetSwitcherRotation();
    }
}
