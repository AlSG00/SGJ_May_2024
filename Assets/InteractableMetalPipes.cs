using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMetalPipes : InteractableItem, IDroppable
{
    //[SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    private Quaternion _rotationOffset;
    private Rigidbody _rigidBody;

    public static event System.Action<Transform, Vector3, Quaternion> PickingPipes;
    public static event System.Action PipesUsed;

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

        PickingPipes?.Invoke(transform, _inHandPositionOffset, Quaternion.Euler(_inHandRotationOffset));
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
        PipesUsed?.Invoke();
        Destroy(gameObject);
    }
}