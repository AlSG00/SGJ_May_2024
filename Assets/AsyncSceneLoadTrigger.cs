using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoadTrigger : MonoBehaviour
{
    public enum LoadMode
    {
        Load,
        Unload
    }
    private bool _triggerActivated = false;
    [SerializeField] private LoadMode _loadModeType;
    [SerializeField] private int _scene;
    [SerializeField] private Collider _ceiling;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggerActivated)
        {
            return;
        }
        gameObject.GetComponent<Collider>().enabled = false;

        _triggerActivated = true;

        if (other.CompareTag("Player") == false)
        {
            return;
        }

        if (_loadModeType == LoadMode.Load)
        {
            SceneManager.LoadScene(_scene, LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.UnloadSceneAsync(_scene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            _ceiling.enabled = true;
        }
    }
}
