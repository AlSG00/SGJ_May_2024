using Cinemachine;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField] private Rigidbody _character;
    [SerializeField] private float _sensitivity = 2;
    [SerializeField] private float _smoothing = 1.5f;

    [Header("Camera Shaking parameters")]
    [SerializeField] private CinemachineVirtualCamera _cmCamera;
    [SerializeField] private float _idleAmplitudeGain = 0.4f;
    [SerializeField] private float _idleFrequencyGain = 0.45f;
    [SerializeField] private float _walkAmplitudeGain;
    [SerializeField] private float _walkFrequencyGain;
    [SerializeField] private float _runAmplitudeGain;
    [SerializeField] private float _runFrequencyGain;
    [SerializeField] private float _amplitudeInterplationStep;
    [SerializeField] private float _frequencyInterplationStep;

    [SerializeField] private GameSettings _gameSettings;
    private Vector2 _velocity;
    private Vector2 _frameVelocity;
    private CinemachineBasicMultiChannelPerlin _cmCameraPerlin;
    private bool _isChangingGain = false;
    [SerializeField] private bool _isMoving;

    internal bool Enabled = false;

    private void OnEnable()
    {
        SetMouseSensitivitySlider.SensitivityChanged += SetSensitivity;
        SetMouseSmoothingSlider.SmoothingChanged += SetSmoothing;
        FirstPersonMovement.Moving += SetMovementShaking;
    }

    private void OnDisable()
    {
        SetMouseSensitivitySlider.SensitivityChanged -= SetSensitivity;
        SetMouseSmoothingSlider.SmoothingChanged -= SetSmoothing;
        FirstPersonMovement.Moving -= SetMovementShaking;
    }

    void Start()
    {
        _sensitivity = _gameSettings.MouseSensitivity;
        _smoothing = _gameSettings.MouseSmoothing;
        //Cursor.lockState = CursorLockMode.Locked;
        _character.transform.localRotation = Quaternion.AngleAxis(-198, Vector3.up);
        _cmCameraPerlin = _cmCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        PlayerSpawn();
    }

    void Update()
    {
        if (Enabled == false)
        {
            return;
        }

        Look();
    }

    private void Look()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _sensitivity);
        Vector2 rawFrameVelocity = mouseDelta * (Vector2.one * _sensitivity);
        _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, (1 / _smoothing) * Time.deltaTime);
        _velocity += _frameVelocity;
        _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);

        var xQuat = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(_velocity.y, Vector3.left);
        transform.localRotation = xQuat * yQuat;
        //transform.localRotation =
        //transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);
        //transform.localRotation = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        //_character.transform.localRotation = Quaternion.AngleAxis(/*_character.localRotation.x + */_velocity.x, Vector3.up);
    }

    private void SetSensitivity(float value)
    {
        _sensitivity = value;
    }

    private void SetSmoothing(float value)
    {
        Debug.Log("smooooooth");
        _smoothing = value;
    }

    private async void PlayerSpawn()
    {
        await Task.Delay(2000);
        Enabled = true;
    }

    #region CAMERA SHAKING

    public void SetMovementShaking(FirstPersonMovement.Movement movementType)
    {
        float _targetAmplitudeGain = 0;
        float _targetFrequencyGain = 0;
        bool isIncreasing = false;

        if (movementType == FirstPersonMovement.Movement.Idle)
        {
            _targetAmplitudeGain = _idleAmplitudeGain;
            _targetFrequencyGain = _idleFrequencyGain;
        }
        else if (movementType == FirstPersonMovement.Movement.Walk)
        {
            _targetAmplitudeGain = _walkAmplitudeGain;
            _targetFrequencyGain = _walkFrequencyGain;
        }
        else if (movementType == FirstPersonMovement.Movement.Run)
        {
            _targetAmplitudeGain = _runAmplitudeGain;
            _targetFrequencyGain = _runFrequencyGain;
        }

        if (_targetAmplitudeGain > _cmCameraPerlin.m_AmplitudeGain)
        {
            isIncreasing = true;
        }

        if (isIncreasing)
        {
            if (_isChangingGain == false)
            {
                _isChangingGain = true;
                StartCoroutine(IncreaseCameraAmplitudeRoutine(_targetAmplitudeGain));
                StartCoroutine(IncreaseCameraFrequencyRoutine(_targetFrequencyGain));
            }
        }
        else
        {
            if (_isChangingGain == false)
            {
                _isChangingGain = true;
                StartCoroutine(DecreaseCameraAmplitudeRoutine(_idleAmplitudeGain));
                StartCoroutine(DecreaseCameraFrequencyRoutine(_idleFrequencyGain));
            }
        }
    }

    private IEnumerator IncreaseCameraAmplitudeRoutine(float targetAmplitudeGain)
    {
        while (_cmCameraPerlin.m_AmplitudeGain < targetAmplitudeGain)
        {
            _cmCameraPerlin.m_AmplitudeGain += _amplitudeInterplationStep;
            yield return new WaitForFixedUpdate();
        }

        _cmCameraPerlin.m_AmplitudeGain = targetAmplitudeGain;
    }
    private IEnumerator IncreaseCameraFrequencyRoutine(float targetFrequencyGain)
    {
        while (_cmCameraPerlin.m_FrequencyGain < targetFrequencyGain)
        {
            _cmCameraPerlin.m_FrequencyGain += _frequencyInterplationStep;
            yield return new WaitForFixedUpdate();
        }

        _cmCameraPerlin.m_FrequencyGain = targetFrequencyGain;
        _isChangingGain = false;
    }

    private IEnumerator DecreaseCameraAmplitudeRoutine(float targetAmplitudeGain)
    {
        while (_cmCameraPerlin.m_AmplitudeGain > targetAmplitudeGain)
        {
            _cmCameraPerlin.m_AmplitudeGain -= _amplitudeInterplationStep;
            yield return new WaitForFixedUpdate();
        }

        _cmCameraPerlin.m_AmplitudeGain = targetAmplitudeGain;
    }

    private IEnumerator DecreaseCameraFrequencyRoutine(float targetFrequencyGain)
    {
        while (_cmCameraPerlin.m_FrequencyGain > targetFrequencyGain)
        {
            _cmCameraPerlin.m_FrequencyGain -= _frequencyInterplationStep;
            yield return new WaitForFixedUpdate();
        }

        _cmCameraPerlin.m_FrequencyGain = targetFrequencyGain;
        _isChangingGain = false;
    }

    #endregion
}
