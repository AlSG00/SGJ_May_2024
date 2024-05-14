using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private FirstPersonMovement _movement;
    [SerializeField] private FirstPersonLook _look;
    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private int _showElementDelay;
    private bool _isVisible;


    private void Start()
    {
        _isVisible = false;
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }

            _isVisible = !_isVisible;
        }
    }

    public void Show()
    {
        Time.timeScale = 0;

        //_movement.enabled = false;
        _look.Enabled = false;
        Cursor.lockState = CursorLockMode.None;
        foreach (var element in _uiElements)
        {
            element.SetActive(true);
        }
    }

    public void Hide()
    {
        Time.timeScale = 1;

        //_movement.enabled = true;
        _look.Enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var element in _uiElements)
        {
            element.SetActive(false);
        }
    }
}
