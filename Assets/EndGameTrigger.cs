using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    public static event System.Action Endgame;
    public AudioSource Audio;
    public AudioClip Clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }
        //Audio.PlayOneShot(Clip);
        Endgame?.Invoke();
    }
}
