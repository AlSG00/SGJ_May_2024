using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDisabler : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioSourceList;

    private void OnEnable()
    {
        EndGameTrigger.Endgame += Activate;
    }

    private void OnDisable()
    {
        EndGameTrigger.Endgame -= Activate;
    }

    private void Activate()
    {
        foreach (var audio in _audioSourceList)
        {
            audio.enabled = false;
        }
    }
}
