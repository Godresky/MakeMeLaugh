using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickableItem : MonoBehaviour
{
    private Transform _objectGrabPoint = null;
    private Rigidbody _rigidbody;

    public Action OnWantDrop;

    [SerializeField]
    private float _lerpSpeed = 200f;
    [SerializeField]
    private float _dragModifier = 2f;

    private float _distanceForDrop = 1.4f;
    private float _oldDrag;

    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        _objectGrabPoint = null;

        _rigidbody.drag = _oldDrag;

        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Grab(Transform transform)
    {
        _objectGrabPoint = transform;

        _oldDrag = _rigidbody.drag;

        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovementToGrabPoint();
    }

    private void MovementToGrabPoint()
    {
        if (_objectGrabPoint != null)
        {
            float distance = Vector3.Distance(_objectGrabPoint.position, transform.position);
            Vector3 direction = (_objectGrabPoint.transform.position - transform.position).normalized;

            if (distance >= _distanceForDrop)
            {
                OnWantDrop?.Invoke();
            }

            if (distance >= 0.01f)
            {
                _rigidbody.AddForce(direction * _lerpSpeed * distance);
                _rigidbody.drag = _dragModifier / distance;
            }
        }
    }
}