using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDirt : InteractableItem
{
    [SerializeField] private bool _enabledOnStart;
    [SerializeField] private AudioSource _dirtAudioSource;
    [SerializeField] private Item requiredItem;
    [SerializeField] private int _remainingDigs;
    [SerializeField] private Collider[] _nextTrigger;

    private void Start()
    {
        gameObject.GetComponent<Collider>().enabled = _enabledOnStart;
    }

    public override void Interact()
    {
        RemoveDirt();       
    }

    private async void RemoveDirt()
    {
        await System.Threading.Tasks.Task.Delay(700);

        _remainingDigs--;
        if (_remainingDigs <= 0)
        {
            if (_nextTrigger.Length != 0)
            {
                foreach (var trigger in _nextTrigger)
                {
                    trigger.enabled = true;
                }
            }
            //if (trigger != null)
            //{
            //        trigger.enabled = true;
            //}
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
