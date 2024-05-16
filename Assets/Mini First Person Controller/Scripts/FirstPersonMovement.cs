﻿using System;
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
    [SerializeField] private LayerMask _layer;

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
        CheckGround();
        CheckMovementState();
    }

    RaycastHit hit;
    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, _layer) == false)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y - 0.97f, _rigidbody.velocity.z);
        }
    }

    private void FixedUpdate()
    {
        Move();

        Vector2 targetVelocity = new Vector2(_horizontalInput /** _targetMovingSpeed*/, _verticalInput /** _targetMovingSpeed*/);
        //_rigidbody.AddForce(transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y), ForceMode.VelocityChange);

        //_rigidbody.velocity = transform.rotation * /*new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);*/

        

        Quaternion rotation = Quaternion.Euler(0, _cameraLook.transform.rotation.eulerAngles.y, 0);
        Vector3 moveDirection = (rotation * new Vector3(targetVelocity.x, 0, targetVelocity.y));

        _rigidbody.velocity = moveDirection * _targetMovingSpeed/* _cameraLook.transform*/;
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