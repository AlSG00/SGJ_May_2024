using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerFlashlight _flashlight;
    [SerializeField] private ObjectInteraction _interaction;
    [SerializeField] private FirstPersonLook _look;
    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private GameObject _settingsMenu;
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
        }
    }

    public void Show()
    {
        Time.timeScale = 0;
        _isVisible = true;
        _look.Enabled = false;
        _flashlight.enabled = false;
        _interaction.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        foreach (var element in _uiElements)
        {
            element.SetActive(true);
        }
    }

    public void Hide()
    {
        Time.timeScale = 1;
        _isVisible = false;
        _look.Enabled = true;
        _flashlight.enabled = true;
        _interaction.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        foreach (var element in _uiElements)
        {
            element.SetActive(false);
        }

        if (_settingsMenu.activeSelf)
        {
            _settingsMenu.SetActive(false);
        }
    }
}
