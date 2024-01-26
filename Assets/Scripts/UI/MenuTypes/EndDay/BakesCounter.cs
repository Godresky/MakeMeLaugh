using UnityEngine;
using UnityEngine.Events;
public class BakesCounter : MonoBehaviour
{
    private int _count = 0;
    public event UnityAction<int> BakesCountChanged;
    public int Count{
        get{
            return _count;
        }
        set{
            _count = value;
            BakesCountChanged?.Invoke(_count);
        }
    }
}
