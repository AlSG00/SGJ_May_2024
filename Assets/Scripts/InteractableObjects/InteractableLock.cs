using UnityEngine;

public class InteractableLock : InteractableItem
{
    [SerializeField] private InteractableItem[] _connectedInteractables;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    public override void Interact()
    {
        _audioSource.PlayOneShot(_audioClip);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        foreach (var interactable in _connectedInteractables)
        {
            interactable.CanInteract = true;
        }

        Destroy(gameObject, 3f);
    }


}
