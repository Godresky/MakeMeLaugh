using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private Transform _conteiner;
    private Transform _itemParent;
    private Collider _item;
    private Rigidbody _rb;
    private BoxCollider _coll;


    [SerializeField]
    private bool _equipped = false;
    [SerializeField]
    private bool _pickingUp = false;

    private void OnTriggerStay(Collider other)
    {
        if (!_equipped && _pickingUp && other.gameObject.tag == "Pickable")
        {
            _equipped = true;
            _item = other;
            _itemParent = _item.transform.parent;
            _item.transform.SetParent(_conteiner);

            _item.transform.localPosition = Vector3.zero;
            _item.transform.localRotation = Quaternion.Euler(Vector3.zero);

            _rb = _item.GetComponent<Rigidbody>();
            _coll = _item.GetComponent<BoxCollider>();
            _rb.isKinematic = true;
            _coll.isTrigger = true;
        }
    }

    public void SwitchPickingUp()
    {
        _pickingUp = !_pickingUp;
    }

    private void Update()
    {
        if (_equipped && !_pickingUp)
        {
            _equipped = false;
            _item.transform.SetParent(_itemParent);

            _rb.isKinematic = false;
            _coll.isTrigger = false;
        }
    }
}
