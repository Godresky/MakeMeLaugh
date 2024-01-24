using UnityEngine;

public class Egg : DoughIngridient
{
    public bool IsBroken { get ; set; }

    [SerializeField]
    private float _magnitudeBroken = 1.2f;

    private Rigidbody _rigidbody;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_rigidbody.angularVelocity.magnitude > _magnitudeBroken)
        {
            IsBroken = true;
            Debug.Log(_rigidbody.angularVelocity.magnitude);
        }
    }
}
