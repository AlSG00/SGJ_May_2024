using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public string HintMessage;
    public string CantInteractMessage;
    public string NeedItemMessage;
    public string WrongItemMessage;
    public string InteractionSuccesfulMessage;
    public bool CanInteract = true;
    public Item ItemType;
    public bool RequiresAnItem;
    public Item RequiredItemType;
    public bool IsUsing { get; protected set; }
    public AudioClip InteractAudio
    {
        get
        {
            return _internalAudio;
        }
    }
    [SerializeField] protected AudioClip _internalAudio;

    public AudioClip UseAudio
    {
        get
        {
            return _useAudio;
        }
    }
    [SerializeField] protected AudioClip _useAudio;

    public abstract void Interact();

    public virtual async void UseAsync() { }

    public virtual void ApplyOtherItem() { }
}
