using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Transform _objectGrabPoint = null;
    private Rigidbody _rigidbody;

    private float _distanceRaycast = 100f;
    [SerializeField]
    private LayerMask _layerMaskRaycast;

    private float _lerpSpeed = 10f;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Drop()
    {
        _objectGrabPoint = null;

        _rigidbody.useGravity = true;
        _rigidbody.freezeRotation = false;
        _rigidbody.velocity = Vector3.zero;
    }

    public void Grab(Transform transform)
    {
        _objectGrabPoint = transform;

        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        CheckRaycast();
    }

    private void CheckRaycast()
    {
        if (_objectGrabPoint != null)
        {
            Ray ray = new Ray(transform.position, _objectGrabPoint.transform.position);

            if (Physics.Raycast(ray, out RaycastHit hit, _distanceRaycast, _layerMaskRaycast, QueryTriggerInteraction.Ignore) && hit.collider.gameObject != gameObject)
            {
                Debug.Log(hit.collider.gameObject);

                if (Vector3.Distance(hit.transform.position, transform.position) < 1f)
                    return;
            }

            Vector3 newPosition = Vector3.Lerp(transform.position, _objectGrabPoint.transform.position, Time.fixedDeltaTime * _lerpSpeed);
            _rigidbody.MovePosition(newPosition);
        }
    }
}