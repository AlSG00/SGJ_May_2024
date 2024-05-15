using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreLayerCollision : MonoBehaviour
{
    private void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }
}
