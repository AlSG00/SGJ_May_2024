using UnityEngine;

public class EndgameReactor : MonoBehaviour
{
    //[SerializeField] private AudioListener _listener;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] FootstepAudioController _footstepsAudio;
    [SerializeField] private FirstPersonMovement _movement;
    [SerializeField] private PlayerFlashlight _flashlight;
    [SerializeField] private GameObject[] _objectsToActivate;
    [SerializeField] private GameObject[] _objectsToDeactivate;

    private void OnEnable()
    {
        EndGameTrigger.Endgame += Activate;
    }

    private void OnDisable()
    {
        EndGameTrigger.Endgame += Activate;
    }

    private async void Activate()
    {
        foreach (var obj in _objectsToActivate)
        {
            obj.SetActive(true);
        }

        foreach (var obj in _objectsToDeactivate)
        {
            obj.SetActive(false);
        }

        _footstepsAudio.enabled = false;
        _movement.enabled = false;
        _flashlight.enabled = false;
        _audioSource.PlayOneShot(_clip);
        // _listener.enabled = false;

        await System.Threading.Tasks.Task.Delay(4000);

        foreach (var obj in _objectsToActivate)
        {
            obj.SetActive(false);
        }

        await System.Threading.Tasks.Task.Delay(500);

        Application.Quit();
    }
}


