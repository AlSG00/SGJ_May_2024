using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRope : InteractableItem, IDroppable
{
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    [SerializeField] private Vector3 _inHandDropPositionOffset;
    [SerializeField] private Vector3 _inHandDropRotationOffset;
    private Quaternion _rotationOffset;
    private Rigidbody _rigidBody;

    public static event System.Action<Transform, Vector3, Vector3, Vector3, Vector3> PickingRope;
    public static event System.Action RopeUsed;

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = false;
        }


        PickingRope?.Invoke(transform, _inHandPositionOffset, _inHandRotationOffset, _inHandDropPositionOffset, _inHandDropRotationOffset);
    }

    public void Drop(Vector3 direction)
    {
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(direction, ForceMode.Impulse);
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
    }

    public override async void UseAsync()
    {
        RopeUsed?.Invoke();
        Destroy(gameObject);
    }
}
