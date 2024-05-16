using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : InteractableItem, IDroppable
{
    [SerializeField] private Renderer[] _meshes;
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    [SerializeField] private Vector3 _inHandDropPositionOffset;
    [SerializeField] private Vector3 _inHandDropRotationOffset;
    private Rigidbody _rigidBody;
    

    public static event System.Action<Transform, Vector3, Vector3, Vector3, Vector3> PickingKey;
    public static event System.Action KeyUsed;

    private void Awake()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public override void Interact()
    {
        if (CanInteract == false)
        {
            return;
        }

        _rigidBody.isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }

        foreach (var mesh in _meshes)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        PickingKey?.Invoke(gameObject.transform, _inHandPositionOffset, _inHandRotationOffset, _inHandDropPositionOffset, _inHandDropRotationOffset);
    }

    public void Drop(Vector3 direction)
    {
        _rigidBody.isKinematic = false;
        _rigidBody.AddForce(direction, ForceMode.Impulse);
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
        }

        foreach (var mesh in _meshes)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

    public override async void UseAsync() 
    {
        CanInteract = false;
        await System.Threading.Tasks.Task.Delay(300);
        KeyUsed?.Invoke();
        Destroy(gameObject, 3f);
    }
}
