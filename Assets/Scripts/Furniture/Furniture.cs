using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private Transform _door;

    protected bool IsClosed = false;
    public void Close(){
        IsClosed = true;
        _door.Rotate(90, 0, 0); // кут можете задати самі бо я хз , так як графіки не бачив і координат теж
    }

    public void Open(){
        IsClosed= false;
        _door.Rotate(90, 0, 0); // кут можете задати самі бо я хз , так як графіки не бачив і координат теж
    }
}
