using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [SerializeField] private bool _isAvailable;
    [SerializeField] private Light _flashlight;
    [SerializeField] private GameObject _flashlightObject;

    [Header("Flashlight audio")]
    [SerializeField] private AudioSource _flashlightAudioSource;
    [SerializeField] private AudioClip _turnOnAudio;
    [SerializeField] private AudioClip _turnOffAudio;

    //[SerializeField] private float _intensity;
    [SerializeField] private bool _isActivated;

    private void OnEnable()
    {
        InteractableFlashlight.EnablingFlashlight += Enable;
    }

    private void OnDisable()
    {
        InteractableFlashlight.EnablingFlashlight -= Enable;
    }

    private void Start()
    {
        SetFlashlightState(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            HandleFlashlightState();
        }
    }
    private void Enable()
    {
        _isAvailable = true;
        _isActivated = false;
    }

    private void HandleFlashlightState()
    {
        if (_isAvailable == false)
        {
            return;
        }

        if (_isActivated == false)
        {
            _isActivated = true;
            _flashlightAudioSource.PlayOneShot(_turnOnAudio);
        }
        else
        {
            _isActivated = false;
            _flashlightAudioSource.PlayOneShot(_turnOffAudio);
        }

        SetFlashlightState(_isActivated);
    }

    private void SetFlashlightState(bool isActive)
    {
        _isActivated = isActive;
        _flashlightObject.SetActive(isActive);
    }
}
