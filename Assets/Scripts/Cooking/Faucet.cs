using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Faucet : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private AudioClip _soundFaucetOpening;
    [SerializeField] private AudioClip _soundFaucetWorking;
    [SerializeField] private AudioClip _soundFaucetClosing;

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

            _audioSource.PlayOneShot(_soundFaucetOpening);

            _audioSource.loop = true;
            _audioSource.clip = _soundFaucetWorking;
        }
        else
        {
            _audioSource?.Stop();

            _audioSource.PlayOneShot(_soundFaucetClosing);
        }
    }

    private void Update()
    {
        if (_isOpen && !_audioSource.isPlaying)
        {
            _audioSource.Play();
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
