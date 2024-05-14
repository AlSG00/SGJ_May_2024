using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] private int _sceneIndexToLoad;

    public void OnClick()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(_sceneIndexToLoad/*, LoadSceneMode.Single*/);
        Debug.Log("Loaded");
    }
}
