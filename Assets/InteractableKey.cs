using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : InteractableItem, IDroppable
{
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Quaternion _inHandRotationOffset;

    public static event System.Action<Transform, Vector3, Quaternion> PickingKey;

    public override void Interact()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
        PickingKey?.Invoke(gameObject.transform, _inHandPositionOffset, _inHandRotationOffset);
    }

    public void Drop()
    {
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
