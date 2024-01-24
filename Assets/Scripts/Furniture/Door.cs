using UnityEngine;

public class Door : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private Vector3 _angle;

    private bool _isOpen = false;
    public bool IsOpen => _isOpen;

    private void Open(){
        _isOpen = true;
        transform.Rotate(_angle);
    }

    private void Close(){
        _isOpen = false;
        transform.Rotate(new Vector3(0, 0, 0));
    }

    public void Interact(){
        if (_isOpen == true)
            Close();
        Open();
    }
}
