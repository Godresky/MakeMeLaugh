using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickingUp : MonoBehaviour
{
    [SerializeField]
    private bool _equipped = false;
    [SerializeField]
    private Transform _cameraTransform;

    private GameObject _pointConteiner;
    private GameObject _equippedItem;
    private Transform _itemParent;
    private Rigidbody _itemRb;

    //private void Highlight()
    //{
    //    // Highlighting of obj
    //    Debug.DrawRay(_playerCameraTransform.position, _playerCameraTransform.forward * _hitRange, Color.red);
    //    if (_hit.collider != null)
    //    {
    //        _hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
    //    }

    //    if (Physics.Raycast(
    //        _playerCameraTransform.position,
    //        _playerCameraTransform.forward,
    //        out _hit,
    //        _hitRange,
    //        _pickableLayerMask))
    //    {
    //        _hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

    //    }
    //}

    public void Drop()
    {
        if (_equipped)
        {
            _equipped = false;

            _equippedItem.transform.SetParent(_itemParent);
            Destroy(_pointConteiner);

            _itemRb.isKinematic = false;
        }
    }

    public void PickUp(GameObject item)
    {
        if (!_equipped)
        {
            _equipped = true;

            _equippedItem = item;
            _itemParent = _equippedItem.transform.parent;

            _pointConteiner = new GameObject("PointConteiner");
            _pointConteiner.transform.SetParent(_cameraTransform);

            _equippedItem.transform.SetParent(_pointConteiner.transform);

            _itemRb = item.GetComponent<Rigidbody>();
            _itemRb.isKinematic = true;
        }
    }

    private void Update()
    {
//Highlight();
    }
}
