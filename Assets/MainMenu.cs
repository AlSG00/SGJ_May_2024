using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] private GameObject _localeStartMenu;
    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private int _showElementDelay;

    private void OnEnable()
    {
       // SetLocaleButton.LocalizationPicked += Show;
    }

    private void OnDisable()
    {
        //SetLocaleButton.LocalizationPicked -= Show;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Show();
    }

    private async void Show()
    {
        //_localeStartMenu.SetActive(false);
        foreach (var element in _uiElements)
        {
            element.SetActive(true);
            await Task.Delay(_showElementDelay);
        }
    }

    private void Hide()
    {
        foreach (var element in _uiElements)
        {
            element.SetActive(false);
        }
    }
}
