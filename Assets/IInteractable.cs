using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public Item ItemType;

    public Item RequiredItemType;

    public bool RequiresAnItem;

   // public static event System.Action<Transform, Vector3, Quaternion> Pick;

    public abstract void Interact();

    public virtual async void UseAsync(/*InteractableItem item*/) { }

    public virtual void ApplyOtherItem() { }
}
