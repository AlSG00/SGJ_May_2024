using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInteractableItemTrigger : InteractableItem
{
    [SerializeField] private bool _enabledOnStart;
    [SerializeField] private GameObject[] ItemsToShow;
    [SerializeField] private Collider[] _colliderArray;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private Collider[] _nextTrigger;
    [SerializeField] private GameObject[] _itemsToDisable;

    private void Awake()
    {
        foreach (var collider in _colliderArray)
        {
            collider.enabled = _enabledOnStart;
        }

        SetItemsVisibility(_enabledOnStart);
    }

    public override void Interact()
    {
        _audioSource?.PlayOneShot(_audioClip);
        SetItemsVisibility(true);
        foreach (var collider in _colliderArray)
        {
            collider.enabled = false;
        }
        if (_nextTrigger.Length != 0)
        {
            foreach (var trigger in _nextTrigger)
            {
                trigger.enabled = true;
            }
        }
        DisableItems();
    }

    private void SetItemsVisibility(bool show)
    {
        foreach (var item in ItemsToShow)
        {
            item.SetActive(show);
        }
    }

    private void DisableItems()
    {
        foreach (var item in _itemsToDisable)
        {
            item.SetActive(false);
        }
    }
}
