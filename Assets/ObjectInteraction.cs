using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private InteractWithWearableItem _wearableItem;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private float _raycastDistance;
    private RaycastHit _hit;
    private const string interactableTag = "Interactable";

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
                    if (interactable.RequiresAnItem)
                    {
                        wearable = _wearableItem.GetCurrentItem();
                        if (wearable is null)
                        {
                            return;
                        }

                        if (wearable.ItemType != interactable.RequiredItemType)
                        {
                            return;
                        }

                        wearable.UseAsync();
                    }

                    interactable.Interact();

                }
            }
        }
    }
}
