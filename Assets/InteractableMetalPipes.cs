using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMetalPipes : InteractableItem, IDroppable
{
    [SerializeField] private Renderer[] _meshes;
    //[SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    [SerializeField] private Vector3 _inHandDropPositionOffset;
    [SerializeField] private Vector3 _inHandDropRotationOffset;
    //private Quaternion _rotationOffset;
    public Quaternion _droppedRotationOffset { get; private set; }
    private Rigidbody _rigidBody;

    public static event System.Action<Transform, Vector3, Vector3, Vector3, Vector3> PickingPipes;
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

        foreach (var mesh in _meshes)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        PickingPipes?.Invoke(transform, _inHandPositionOffset, _inHandRotationOffset, _inHandDropPositionOffset, _inHandDropRotationOffset);
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
        PipesUsed?.Invoke();
        Destroy(gameObject);
    }
}
