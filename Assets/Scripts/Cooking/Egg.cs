using UnityEngine;

[RequireComponent (typeof(FridgeItem))]
[RequireComponent (typeof(AudioSource))]
public class Egg : MonoBehaviour
{
    [SerializeField]
    private float _magnitudeBroken = 1.2f;

    [SerializeField]
    private AudioSource _audioSource;

    private FridgeItem _fridgeItem;

    private void Awake(){
        _fridgeItem = GetComponent<FridgeItem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude > _magnitudeBroken)
        {
            _audioSource.Play();
            Fridge.Singleton.UpdateFridgeItem(_fridgeItem);

        }
    }
}
