using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : InteractableItem, IDroppable
{
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Quaternion _inHandRotationOffset;
    private Rigidbody _rigidBody;

    public static event System.Action<Transform, Vector3, Quaternion> PickingKey;
    public static event System.Action KeyUsed;

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        _rigidBody.isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
        PickingKey?.Invoke(gameObject.transform, _inHandPositionOffset, _inHandRotationOffset);
    }

    public void Drop()
    {
        _rigidBody.isKinematic = false;
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
    }

    public override async void UseAsync() 
    {
        await System.Threading.Tasks.Task.Delay(300);
        KeyUsed?.Invoke();
        Destroy(gameObject, 3f);
    }
}
