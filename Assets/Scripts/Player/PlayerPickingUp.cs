using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPickingUp : MonoBehaviour
{
    [SerializeField]
    private bool _equipped = false;
    [SerializeField]
    private Transform _objectGrabPoint;

    [SerializeField]
    private AudioSource _getItemSound;
    [SerializeField]
    private AudioSource _dropItemSound;

    private PickableItem _equippedItem;

    public bool Equipped { get => _equipped; }

    public void Drop()
    {
        if (_equipped)
        {
            _equipped = false;

            _equippedItem.Drop();
            _equippedItem.OnWantDrop -= Drop;

            _equippedItem = null;

            if (!_dropItemSound.isPlaying)
                _dropItemSound.Play();

        }
    }

    public void Grab(PickableItem item)
    {
        if (!_equipped)
        {
            _equipped = true;
            _equippedItem = item;

            _objectGrabPoint.position = _equippedItem.transform.position;

            _equippedItem.Grab(_objectGrabPoint.transform);
            _equippedItem.OnWantDrop += Drop;

            if (!_getItemSound.isPlaying)
                _getItemSound.Play();
        }
    }

    public PickableItem GetEquippedItem()
    {
        return _equipped ? _equippedItem : null;
    }

}
