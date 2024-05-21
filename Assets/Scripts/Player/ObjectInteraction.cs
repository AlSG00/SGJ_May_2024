using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private InteractWithWearableItem _wearableItem;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private LayerMask _layer;
    private RaycastHit _hit;
    private const string interactableTag = "Interactable";

    public static event System.Action<string> InteractResult;
    public static event System.Action<string> InteractHint;
    public static event System.Action<AudioClip> InteractResultAudio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out _hit, _raycastDistance, _layer))
            {
                if (_hit.transform.CompareTag(interactableTag))
                {
                    InteractableItem interactable = _hit.transform.gameObject.GetComponent<InteractableItem>();
                    InteractableItem wearable = null;
                    if (interactable.CanInteract == false)
                    {
                        if (interactable.CantInteractLocalizedMessage.IsEmpty == false)
                        {
                            InteractResult?.Invoke(interactable.CantInteractLocalizedMessage.GetLocalizedString());
                        }

                        return;
                    }

                    if (interactable.RequiresAnItem)
                    {
                        wearable = _wearableItem.GetCurrentItem();
                        if (wearable == null)
                        {
                            if (interactable.NeedItemLocalizedMessage.IsEmpty == false)
                            {
                                InteractResult?.Invoke(interactable.NeedItemLocalizedMessage.GetLocalizedString());
                            }
                            
                            return;
                        }

                        if (wearable.ItemType != interactable.RequiredItemType)
                        {
                            if (interactable.WrongItemLocalizedMessage.IsEmpty == false)
                            {
                                InteractResult?.Invoke(interactable.WrongItemLocalizedMessage.GetLocalizedString());
                            }

                            return;
                        }

                        if (wearable != null & wearable.IsUsing)
                        {
                            return;
                        }

                        InteractResultAudio?.Invoke(wearable.UseAudio);
                        if (interactable.HintLocalizedMessage.IsEmpty == false)
                        {
                            InteractHint?.Invoke(interactable.HintLocalizedMessage.GetLocalizedString());
                        }

                        wearable.UseAsync();
                        interactable.Interact();

                        if (interactable.InteractionSuccesfulLocalizedMessage.IsEmpty == false)
                        {
                            InteractResult?.Invoke(interactable.InteractionSuccesfulLocalizedMessage.GetLocalizedString());
                            //interactable.InteractionSuccesfulLocalizedMessage.;
                        }
                        return;
                    }

                    if (interactable.HintLocalizedMessage.IsEmpty == false)
                    {
                        InteractHint?.Invoke(interactable.HintLocalizedMessage.GetLocalizedString());
                    }

                    if (interactable.InteractionSuccesfulLocalizedMessage.IsEmpty == false)
                    {
                        InteractResult?.Invoke(interactable.InteractionSuccesfulLocalizedMessage.GetLocalizedString());
                        //interactable.InteractionSuccesfulMessage = "";
                    }

                    interactable.Interact();
                    InteractResultAudio?.Invoke(interactable.InteractAudio);
                }
            }
        }
    }
}
