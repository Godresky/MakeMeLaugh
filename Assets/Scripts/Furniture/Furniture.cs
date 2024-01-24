using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private Transform _door;

    public virtual void Interact()
    {

    }

    protected bool IsClosed = false;
    public void Close(){
        IsClosed = true;
        _door.Rotate(90, 0, 0);
    }

    public void Open(){
        IsClosed= false;
        _door.Rotate(90, 0, 0);
    }
}
