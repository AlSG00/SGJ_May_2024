using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;

    private void OnClick()
    {
        _settingsMenu.SetActive(!_settingsMenu.activeSelf);
        
    }
}
