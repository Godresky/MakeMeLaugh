using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEditor.Progress;

public class PlayerPickUpSystem : MonoBehaviour
{
    [Header("Picking Up Settings")]
    [SerializeField]
    private LayerMask _pickableLayerMask;
    [SerializeField]
    private Transform _playerCameraTransform;
    [SerializeField]
    private bool _equipped = false;
    [SerializeField]
    private bool _pickingUp = false;

    [Header("Raycast Settings")]
    [SerializeField]
    [Min(1)]
    private float _hitRange = 3;
    private RaycastHit _hit;
    private GameObject _pointConteiner;
    private Transform _itemParent;
    private Rigidbody _rb;
    private BoxCollider _coll;

    private void Highlight()
    {
        // Highlighting of obj
        Debug.DrawRay(_playerCameraTransform.position, _playerCameraTransform.forward * _hitRange, Color.red);
        if (_hit.collider != null)
        {
            _hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (Physics.Raycast(
            _playerCameraTransform.position,
            _playerCameraTransform.forward,
            out _hit,
            _hitRange,
            _pickableLayerMask))
        {
            _hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);

        }
    }

    private void PickingUp()
    {
        // Picking
        if (_hit.collider != null && !_equipped && _pickingUp)
        {
            _equipped = true;
            _itemParent = _hit.transform.parent;
            _pointConteiner = new GameObject("PointConteiner");
            _pointConteiner.transform.SetParent(_playerCameraTransform);
            _hit.transform.SetParent(_pointConteiner.transform);

            _rb = _hit.collider.GetComponent<Rigidbody>();
            _coll = _hit.collider.GetComponent<BoxCollider>();
            _rb.isKinematic = true;
        }
        else if (!_pickingUp && _equipped)
        {
            _equipped = false;
            _hit.transform.SetParent(_itemParent);
            Destroy(_pointConteiner);
            _rb.isKinematic = false;
        }
    }

    void Update()
    {
        Highlight();
        PickingUp();
    }

    public void Interact()
    {
        _pickingUp = !_pickingUp;
    }   
}
