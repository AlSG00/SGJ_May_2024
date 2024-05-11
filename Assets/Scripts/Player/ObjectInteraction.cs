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
                Debug.Log(_hit.transform.name);
                if (_hit.transform.CompareTag(interactableTag))
                {
                    InteractableItem interactable = _hit.transform.gameObject.GetComponent<InteractableItem>();
                    InteractableItem wearable = null;
                    if (interactable.CanInteract == false)
                    {
                        InteractResult?.Invoke(interactable.CantInteractMessage);
                        return;
                    }

                    if (interactable.RequiresAnItem)
                    {
                        wearable = _wearableItem.GetCurrentItem();
                        if (wearable == null)
                        {
                            if (interactable.NeedItemMessage != "")
                            {
                                InteractResult?.Invoke(interactable.NeedItemMessage);
                            }
                            
                            return;
                        }

                        if (wearable.ItemType != interactable.RequiredItemType)
                        {
                            if (interactable.WrongItemMessage != "")
                            {
                                InteractResult?.Invoke(interactable.WrongItemMessage);
                            }

                            return;
                        }

                        if (wearable != null & wearable.IsUsing)
                        {
                            return;
                        }

                        InteractResultAudio?.Invoke(wearable.UseAudio);
                        if (interactable.HintMessage != "")
                        {
                            InteractHint?.Invoke(interactable.HintMessage);
                        }

                        wearable.UseAsync();
                        interactable.Interact();

                        if (interactable.InteractionSuccesfulMessage != "")
                        {
                            InteractResult?.Invoke(interactable.InteractionSuccesfulMessage);
                            interactable.InteractionSuccesfulMessage = "";
                        }
                        return;
                    }

                    if (interactable.HintMessage != "")
                    {
                        InteractHint?.Invoke(interactable.HintMessage);
                    }

                    if (interactable.InteractionSuccesfulMessage != "")
                    {
                        InteractResult?.Invoke(interactable.InteractionSuccesfulMessage);
                        interactable.InteractionSuccesfulMessage = "";
                    }
                    interactable.Interact();
                    InteractResultAudio?.Invoke(interactable.InteractAudio);
                }
            }
        }
    }
}
