using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public enum Movement
    {
        Idle,
        Walk,
        Run
    }

    public static Movement MovemenType;

    [SerializeField] private FirstPersonLook _cameraLook;
    [SerializeField] private FootstepAudioController _footstepAudio;

    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    private Rigidbody _rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    private float _horizontalInput;
    private float _verticalInput;
    private float _targetMovingSpeed;

    public static event System.Action<Movement> Moving;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
        CheckMovementState();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalInput * _targetMovingSpeed, _verticalInput * _targetMovingSpeed);

        _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _targetMovingSpeed);
    }

    private void Move()
    {
        IsRunning = Input.GetKey(runningKey);

        _targetMovingSpeed = IsRunning ? runSpeed : speed;
        _horizontalInput = Input.GetAxis(_horizontalAxis);
        _verticalInput = Input.GetAxis(_verticalAxis);
    }

    private void CheckMovementState()
    {
        if (Mathf.Abs(Input.GetAxisRaw(_horizontalAxis)) < 1 &&
            Mathf.Abs(Input.GetAxisRaw(_verticalAxis)) < 1)
        {
            MovemenType = Movement.Idle;
        }
        else if (_targetMovingSpeed == speed)
        {
            MovemenType = Movement.Walk;
        }
        else
        {
            MovemenType = Movement.Run;
        }

        Moving?.Invoke(MovemenType);
    }
}