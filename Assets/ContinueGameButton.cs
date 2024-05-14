using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameButton : MonoBehaviour
{
    [SerializeField] private PauseMenu _pauseMenu;

    public void OnClick()
    {
        _pauseMenu.Hide();
    }
}
