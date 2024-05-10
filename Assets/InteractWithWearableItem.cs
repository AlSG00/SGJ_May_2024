using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithWearableItem : MonoBehaviour
{
    public Transform CurrentItem {
    get
        {
            return _currentItem;
        }
    }

    [SerializeField] private Transform _handPivot;
    [SerializeField]  private Transform _currentItem;
    [SerializeField]  private Vector3 _currentPositionOffset;
    [SerializeField]  private Quaternion _currentRotationOffset;

    private void OnEnable()
    {
        InteractableShowel.PickingShovel += GetItem;
        InteractableKey.PickingKey += GetItem;
        InteractableShowel.ShovelBroken += DropItem;
       // InteractableDirt.Digging += ApplyItemInHands;
    }

    private void OnDisable()
    {
        InteractableShowel.PickingShovel -= GetItem;
        InteractableKey.PickingKey -= GetItem;
        InteractableShowel.ShovelBroken -= DropItem;
       // InteractableDirt.Digging -= ApplyItemInHands;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            DropItem();
        }
    }

    private void GetItem(Transform item, Vector3 itemPositionOffset, Quaternion itemRotationOffset)
    {
        if (_currentItem is not null)
        {
            Debug.Log("Hands are busy");
        }

        item.parent = _handPivot;
        item.localPosition = _handPivot.localPosition + itemPositionOffset;
        item.localRotation = _handPivot.localRotation * itemRotationOffset;
        _currentItem = item;
        item.GetComponent<Rigidbody>().isKinematic = true;

    }

    private void DropItem()
    {
        if (_currentItem is null)
        {
            return;
        }

        _currentItem.SetParent(null);
        _currentItem.GetComponent<IDroppable>().Drop();
        _currentItem = null;
    }

    //private void ApplyItemInHands(Item requiredItemType, InteractableItem interactableDirt)
    //{
    //    if (_currentItem is null)
    //    {
    //        return;
    //    }

    //    InteractableItem item = _currentItem.GetComponent<InteractableItem>();
    //    if (item.ItemType != requiredItemType)
    //    {
    //        return;
    //    }

    //    item.UseAsync(interactableDirt);
    //}

    internal InteractableItem GetCurrentItem()
    {
        InteractableItem item;
        _currentItem.TryGetComponent<InteractableItem>(out item);

        return item;
    }
}
