using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableShowel : InteractableItem, IDroppable
{
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Quaternion _inHandRotationOffset;
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _remainingUses;

    public static event System.Action<Transform, Vector3, Quaternion> PickingShovel;
    public static event System.Action ShovelBroken;

    public override void Interact()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = false;
        }

        PickingShovel?.Invoke(transform, _inHandPositionOffset, _inHandRotationOffset);

    }

    public void Drop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
    }

    private bool _isUsing = false;
    public async override void UseAsync(InteractableItem interactableItem)
    {
        if (_isUsing)
        {
            return;
        }

        _isUsing = true;
        _remainingUses--;
        _animator.SetTrigger("Use");
        await System.Threading.Tasks.Task.Delay(550);
        _audioSource.PlayOneShot(_audioSource.clip);
        await System.Threading.Tasks.Task.Delay(600);
        interactableItem.ApplyOtherItem();
        await System.Threading.Tasks.Task.Delay(600);

        if (_remainingUses <= 0)
        {
            ShovelBroken?.Invoke();
            Destroy(gameObject, 2f);
        }
        _isUsing = false;
        
    }
}
