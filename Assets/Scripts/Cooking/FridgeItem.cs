
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(PickableItem))]
public class FridgeItem : MonoBehaviour
{
    public bool IsUsing = false;

    public Rigidbody Rigidbody;
    public PickableItem PickableItem;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PickableItem = GetComponent<PickableItem>();
    }
}
