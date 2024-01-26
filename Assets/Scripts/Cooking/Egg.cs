using UnityEngine;

[RequireComponent (typeof(FridgeItem))]
[RequireComponent (typeof(AudioSource))]
public class Egg : DoughIngridient
{
    public bool IsBroken { get ; set; }

    [SerializeField]
    private float _magnitudeBroken = 1.2f;

    [SerializeField]
    private AudioSource _audioSource;

    private Rigidbody _rigidbody;
    private FridgeItem _fridgeItem;

    private void Awake(){
        _rigidbody = GetComponent<Rigidbody>();
        _fridgeItem = GetComponent<FridgeItem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_rigidbody.angularVelocity.magnitude > _magnitudeBroken)
        {
            _audioSource.Play();
            Fridge.Singleton.UpdateFridgeItem(_fridgeItem);
        }
    }
}
