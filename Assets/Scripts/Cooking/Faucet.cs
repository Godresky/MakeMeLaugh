using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Faucet : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private AudioClip _soundFaucetWorking;

    private AudioSource _audioSource;

    private bool _isOpen = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        _isOpen = !_isOpen;

        if (_isOpen)
        {
            _audioSource?.Stop();

            _audioSource.loop = true;
            _audioSource.clip = _soundFaucetWorking;
            _audioSource.Play();
        }
        else
        {
            _audioSource?.Stop();
        }
    }

    private void OnTriggerStay(Collider other){
        if (_isOpen)
        {
            if (other.TryGetComponent(out Bowl bowl) && !bowl.HasWater)
            {
                bowl.HasWater = true;
            }
        }
    }


}
