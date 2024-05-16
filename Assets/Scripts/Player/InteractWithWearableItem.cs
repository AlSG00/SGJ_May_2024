using UnityEngine;

public class InteractWithWearableItem : MonoBehaviour
{
    public Transform CurrentItem
    {
        get
        {
            return _currentItem;
        }
    }

    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _handPivot;
    [SerializeField] private Transform _currentItem = null;
    //[SerializeField] private Vector3 _currentPositionOffset;
    //[SerializeField] private Quaternion _currentRotationOffset;
    [SerializeField] private Vector3 _currentDropPositionOffset;
    [SerializeField] private Vector3 _currentDropRotationOffset;


    private void OnEnable()
    {
        InteractableShowel.PickingShovel += GetItem;
        InteractableKey.PickingKey += GetItem;
        InteractablePiton.PickingPiton += GetItem;
        InteractableRope.PickingRope += GetItem;
        InteractableMetalPipes.PickingPipes += GetItem;

        InteractableShowel.ShovelBroken += DropItem;
        InteractableKey.KeyUsed += DropItem;
        InteractablePiton.PitonUsed += DropItem;
        InteractableRope.RopeUsed += DropItem;
        InteractableMetalPipes.PipesUsed += DropItem;
    }

    private void OnDisable()
    {
        InteractableShowel.PickingShovel -= GetItem;
        InteractableKey.PickingKey -= GetItem;
        InteractablePiton.PickingPiton -= GetItem;
        InteractableRope.PickingRope -= GetItem;
        InteractableMetalPipes.PickingPipes -= GetItem;

        InteractableShowel.ShovelBroken -= DropItem;
        InteractableKey.KeyUsed -= DropItem;
        InteractablePiton.PitonUsed -= DropItem;
        InteractableRope.RopeUsed -= DropItem;
        InteractableMetalPipes.PipesUsed -= DropItem;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            DropItem();
        }
    }

    private void GetItem(Transform item, Vector3 itemPositionOffset, Vector3 itemRotationOffset, Vector3 itemDropPositionOffset, Vector3 itemDropRotatioOffset)
    {
        if (_currentItem != null)
        {
            Debug.Log("Hands are busy");
            return;
        }

        item.parent = _handPivot;
        item.localPosition = _handPivot.localPosition + itemPositionOffset;
        item.localRotation = _handPivot.localRotation * Quaternion.Euler(itemRotationOffset);
        _currentDropPositionOffset = itemDropPositionOffset;
        _currentDropRotationOffset = itemDropRotatioOffset;
        _currentItem = item;
    }

    private void DropItem()
    {
        if (_currentItem == null)
        {
            return;
        }

        if (_currentItem.GetComponent<InteractableItem>().IsUsing)
        {
            return;
        }

        _currentItem.SetParent(_playerBody);
        _currentItem.localRotation = _playerBody.localRotation * Quaternion.Euler(_currentDropRotationOffset);
        _currentItem.localPosition = _playerBody.localPosition + _currentDropPositionOffset;
        _currentItem.SetParent(null);
        _currentItem.GetComponent<IDroppable>().Drop(_playerBody.forward);
        _currentItem = null;
    }

    internal InteractableItem GetCurrentItem()
    {
        InteractableItem item = null;
        if (_currentItem != null)
        {
            _currentItem.TryGetComponent<InteractableItem>(out item);
        }
        return item;
    }
}
