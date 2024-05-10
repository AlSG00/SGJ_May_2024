using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFlashlight : InteractableItem
{
    public static event System.Action EnablingFlashlight;

    public override void Interact()
    {
        EnablingFlashlight?.Invoke();
        Destroy(gameObject);
    }
}
