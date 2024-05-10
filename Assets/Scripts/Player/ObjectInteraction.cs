using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private InteractWithWearableItem _wearableItem;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _raycastDistance;
    private RaycastHit _hit;
    private const string interactableTag = "Interactable";

    public static event System.Action<string> InteractResult;
    public static event System.Action<AudioClip> InteractResultAudio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out _hit, _raycastDistance))
            {
                Debug.Log(_hit.transform.name);
                if (_hit.transform.CompareTag(interactableTag))
                {
                    InteractableItem interactable = _hit.transform.gameObject.GetComponent<InteractableItem>();
                    InteractableItem wearable = null;
                    if (interactable.CanInteract == false)
                    {
                        Debug.Log(interactable.CantInteractMessage);
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
                                Debug.Log(interactable.NeedItemMessage);
                                InteractResult?.Invoke(interactable.NeedItemMessage);
                            }
                            
                            return;
                        }

                        if (wearable.ItemType != interactable.RequiredItemType)
                        {
                            if (interactable.CantInteractMessage != "")
                            {
                                Debug.Log(interactable.WrongItemMessage);
                                InteractResult?.Invoke(interactable.WrongItemMessage);
                            }

                            return;
                        }

                        if (wearable != null & wearable.IsUsing)
                        {
                            return;
                        }

                        wearable.UseAsync();
                        interactable.Interact();

                        if (interactable.InteractionSuccesfulMessage != "")
                        {
                            Debug.Log(interactable.InteractionSuccesfulMessage);
                            InteractResult?.Invoke(interactable.InteractionSuccesfulMessage);
                            interactable.InteractionSuccesfulMessage = "";
                        }
                        return;
                    }

                    if (interactable.InteractionSuccesfulMessage != "")
                    {
                        Debug.Log(interactable.InteractionSuccesfulMessage);
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
