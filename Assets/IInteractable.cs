using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public Item itemType;

    public abstract void Interact();

    public virtual async void UseAsync(InteractableItem item) { }

    public virtual void ApplyOtherItem() { }

}
