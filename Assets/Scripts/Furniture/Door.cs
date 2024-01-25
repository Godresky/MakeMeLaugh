using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Door : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private Vector3 _openedRotation;
    [SerializeField] private Vector3 _closedRotation;
    [SerializeField] private Transform _pivotPoint;
    [SerializeField] private float _speedRotation;

    private bool _isOpen = false;
    private bool _isChangingRotation = false;

    public bool IsOpen { get => _isOpen; }

    private void Start()
    {
        if (_pivotPoint == null)
        {
            _pivotPoint = transform;
        }
    }

    private void Open(){
        _isOpen = true;
        _isChangingRotation = true;
        //_pivotPoint.Rotate(_openedRotation);
    }

    private void Close(){
        _isOpen = false;
        _isChangingRotation = true;
        //_pivotPoint.Rotate(_closedRotation);
    }

    public void Interact(){
        if (_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Update()
    {
        if (_isChangingRotation)
        {
            Quaternion quaternionTarget = Quaternion.Euler(_isOpen ? _openedRotation : _closedRotation);
            _pivotPoint.rotation = Quaternion.RotateTowards(_pivotPoint.rotation, quaternionTarget, _speedRotation * Time.deltaTime);

            //if (_pivotPoint.rotation == quaternionTarget)
            //    _isChangingRotation = false;
        }
    }
}
