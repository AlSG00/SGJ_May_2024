using UnityEngine;

public class InteractableShowel : InteractableItem, IDroppable
{
    [SerializeField] private Renderer[] _meshes;
    [SerializeField] private Vector3 _inHandPositionOffset;
    [SerializeField] private Vector3 _inHandRotationOffset;
    [SerializeField] private Vector3 _inHandDropPositionOffset;
    [SerializeField] private Vector3 _inHandDropRotationOffset;
    [SerializeField] private Collider[] colliderArray;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _remainingUses;

    public static event System.Action<Transform, Vector3, Vector3, Vector3, Vector3> PickingShovel;
    public static event System.Action ShovelBroken;

    public override void Interact()
    {
        if (CanInteract == false)
        {
            return;
        }

        GetComponent<Rigidbody>().isKinematic = true;
        foreach (var collider in colliderArray)
        {
            collider.enabled = false;
            collider.gameObject.layer = 11;
        }

        foreach (var mesh in _meshes)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        PickingShovel?.Invoke(transform, _inHandPositionOffset, _inHandRotationOffset, _inHandDropPositionOffset, _inHandDropRotationOffset);

    }

    public void Drop(Vector3 direction)
    {
        if (IsUsing)
        {
            return;
        }

        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;
        rigidBody.AddForce(direction, ForceMode.Impulse);
        foreach (var collider in colliderArray)
        {
            collider.enabled = true;
            collider.gameObject.layer = 9;
        }

        foreach (var mesh in _meshes)
        {
            mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }

    //private bool _isUsing = false;
    public async override void UseAsync(/*InteractableItem interactableItem*/)
    {
        if (IsUsing)
        {
            return;
        }

        IsUsing = true;
        _remainingUses--;
        _animator.SetTrigger("Use");
        await System.Threading.Tasks.Task.Delay(550);
        _audioSource.PlayOneShot(_audioSource.clip);
        await System.Threading.Tasks.Task.Delay(900);

        if (_remainingUses <= 0)
        {
            //gameObject.transform.SetParent(null);
            ShovelBroken?.Invoke();
            CanInteract = false;
            Destroy(gameObject, 2f);
        }

        IsUsing = false;
    }
}
