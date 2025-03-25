using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableItem
{
    [SerializeField] private bool _isOpened;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openAudio;
    [SerializeField] private AudioClip _closeAudio;
    public override void Interact()
    {
        _animator.ResetTrigger("Close");
        _animator.ResetTrigger("Open");
        if (_isOpened)
        {
            _audioSource.PlayOneShot(_closeAudio);
            _animator.SetTrigger("Close");
        }
        else
        {
            _audioSource.PlayOneShot(_openAudio);
            _animator.SetTrigger("Open");
        }

        _isOpened = !_isOpened;
    }
}
