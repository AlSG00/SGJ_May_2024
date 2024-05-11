using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FootstepAudioController : MonoBehaviour
{
    public AudioClip TestSound;
    [SerializeField] private AudioSource _footstepAudioSource;
    [SerializeField] private float _walkFoostepPlayInterval;
    [SerializeField] private float _runFoostepPlayInterval;
    [SerializeField] private float _currentFootstepPlayInterval;

    private bool _readyToPlay = false;
    private float _lastTimePlayed = 0f;
    private int _previousClipIndex;
    [SerializeField] private bool _isMoving;

    [SerializeField] private List<FootstepAudioClip> _footstepAudioClipArray;


    private void OnEnable()
    {
        FirstPersonMovement.Moving += SetMovement;
    }

    private void OnDisable()
    {
        FirstPersonMovement.Moving -= SetMovement;
    }

    private void Update()
    {
        Play();
    }

    private void SetMovement(FirstPersonMovement.Movement movementType)
    {
        if (movementType == FirstPersonMovement.Movement.Idle)
        {
            return;
        }

        if (FirstPersonMovement.MovemenType == FirstPersonMovement.Movement.Walk)
        {
            _currentFootstepPlayInterval = _walkFoostepPlayInterval;
        }
        else
        {
            _currentFootstepPlayInterval = _runFoostepPlayInterval;
        }

    }

    private void Play()
    {
        if (FirstPersonMovement.MovemenType == FirstPersonMovement.Movement.Idle)
        {
            _lastTimePlayed = _currentFootstepPlayInterval / 1.3f;
            return;
        }

        if (_lastTimePlayed < _currentFootstepPlayInterval)
        {
            _lastTimePlayed += Time.deltaTime;
            return;
        }

        string surface = null;
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            Debug.Log($"{hit.transform.name} : {hit.transform.tag}");
            surface = hit.transform.tag;
        }

        if (surface == null)
        {
            return;
        }

        AudioClip[] clipArrayToPlay = _footstepAudioClipArray.Find(clipCollection => clipCollection.Name == surface)._audiocClip;
        int indexToPlay = Random.Range(0, clipArrayToPlay.Length);
        if (indexToPlay == _previousClipIndex)
        {
            indexToPlay = (indexToPlay + 1) % clipArrayToPlay.Length;
        }

        _footstepAudioSource.PlayOneShot(clipArrayToPlay[indexToPlay]);
        _previousClipIndex = indexToPlay;
        _footstepAudioSource.pitch = Random.Range(0.98f, 1.02f);
        _lastTimePlayed = 0f;
    }

    [System.Serializable]
    private struct FootstepAudioClip
    {
        [SerializeField] internal string Name;
        [SerializeField] internal AudioClip[] _audiocClip;
    }
}
