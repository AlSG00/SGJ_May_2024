using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : InteractableItem
{
    public override void Interact()
    {
        Destroy(gameObject);
    }
}
