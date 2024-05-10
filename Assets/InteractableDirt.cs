using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDirt : InteractableItem
{
    [SerializeField] private AudioSource _dirtAudioSource;
    [SerializeField] private Item requiredItem;
    [SerializeField] private int _remainingDigs;
    public static event System.Action<Item, InteractableDirt> Digging;

    public override void Interact()
    {
        //Digging?.Invoke(requiredItem, this);
        RemoveDirt();
       
    }

    private async void RemoveDirt()
    {
        await System.Threading.Tasks.Task.Delay(700);

        _remainingDigs--;
        if (_remainingDigs <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void ApplyOtherItem()
    {
        _remainingDigs--;
        if (_remainingDigs <= 0)
        {
            Destroy(gameObject);
        }
    }
}
