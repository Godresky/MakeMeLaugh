using UnityEngine;

public class Door : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private Vector3 _openedRotation;
    [SerializeField] private Vector3 _closedRotation;
    [SerializeField] private Transform _pivotPoint;

    private bool _isOpen = false;

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
        _pivotPoint.Rotate(_openedRotation);
    }

    private void Close(){
        _isOpen = false;
        _pivotPoint.Rotate(_closedRotation);
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
}
