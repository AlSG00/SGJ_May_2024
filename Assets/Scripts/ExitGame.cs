using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private float _lastClick;
    private float _firstClick;
    private float _secondClick;
    private float _clickDelay = 0.5f;
    private int _clickCount = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _clickCount++;
            
            if (_clickCount == 1)
            {
                _lastClick = Time.time;
            }
            else if (_clickCount == 2)
            {
                if (Time.time - _lastClick < _clickDelay)
                {
                    Application.Quit();
                }

                _clickCount = 0;
            }
        }
    }
}
