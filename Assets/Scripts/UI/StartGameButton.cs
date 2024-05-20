using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private int _sceneIndexToLoad;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _mouseOverAudio;
    [SerializeField] private AudioClip _mouseClickAudio;

    public void OnClick()
    {
        _audioSource.PlayOneShot(_mouseClickAudio);
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadSceneAsync(_sceneIndexToLoad, LoadSceneMode.Single);
        Debug.Log("Loaded");
    }
}
