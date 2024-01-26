
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class FridgeItem : MonoBehaviour
{
    public bool IsUsing = false;

    public Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
}
