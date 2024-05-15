using UnityEngine;

public class InteractablePiton : InteractableItem, IDroppable
{
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    [SerializeField] private Vector3 _inHandDropPositionOffset;
    [SerializeField] private Vector3 _inHandDropRotationOffset;

    private Quaternion _rotationOffset;
    private Rigidbody _rigidBody;

    public static event System.Action<Transform, Vector3, Vector3, Vector3, Vector3> PickingPiton;
    public static event System.Action PitonUsed;

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


        PickingPiton?.Invoke(transform, _inHandPositionOffset, _inHandRotationOffset, _inHandDropPositionOffset, _inHandDropRotationOffset);
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
        PitonUsed?.Invoke();
        Destroy(gameObject);
    }
}
