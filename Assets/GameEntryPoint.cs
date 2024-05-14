using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private int _playerSceneIndex;
    [SerializeField] private int _firstSceneIndex;

    private void Awake()
    {
        SceneManager.LoadScene(_firstSceneIndex, LoadSceneMode.Additive);
        SceneManager.LoadScene(_playerSceneIndex, LoadSceneMode.Additive);
        
    }
}
