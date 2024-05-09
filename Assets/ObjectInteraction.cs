using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
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
                    _hit.transform.gameObject.GetComponent<InteractableItem>().Interact();
                }
            }
        }
    }
}
