using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void OnEnable()
    {
        ObjectInteraction.InteractResultAudio += Play;
    }

    private void OnDisable()
    {
        ObjectInteraction.InteractResultAudio -= Play;
    }

    private void Play(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        _audioSource.PlayOneShot(clip);
        _audioSource.pitch = Random.Range(0.92f, 1.02f);
    }
}
