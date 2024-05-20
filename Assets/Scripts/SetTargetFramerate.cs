using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTargetFramerate : MonoBehaviour
{
    public int TargetValue;

    private void Update()
    {
        if (TargetValue == 0)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = TargetValue;
        }
    }
}
