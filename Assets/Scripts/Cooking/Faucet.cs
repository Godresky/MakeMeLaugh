using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Faucet : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField] private AudioClip _soundFaucetOpening;
    [SerializeField] private AudioClip _soundFaucetWorking;
    [SerializeField] private AudioClip _soundFaucetClosing;

    [SerializeField] private AudioClip _soundValveOpening;
    [SerializeField] private AudioClip _soundValveClosing;

    [SerializeField] private float _toSwitchStateTime;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioSource _valveAudioSource;

    private bool _isOpen = false;
    private bool _isChanging = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Interact()
    {
        StartCoroutine(ToSwitchState());
    }

    private IEnumerator ToSwitchState()
    {
        _isChanging = true;

        _audioSource.Stop();
        _valveAudioSource.Stop();

        if (!_isOpen)
        {
            _valveAudioSource.PlayOneShot(_soundValveOpening);
        }
        else
        {
            _audioSource.loop = false;

            //_audioSource.PlayOneShot(_soundFaucetClosing);
            _valveAudioSource.PlayOneShot(_soundValveClosing);
        }

        yield return new WaitForSeconds(_toSwitchStateTime);

        if (!_isOpen)
        {
            _audioSource.PlayOneShot(_soundFaucetOpening);
            _audioSource.loop = true;
            _audioSource.clip = _soundFaucetWorking;
        }

        _isChanging = false;

        _isOpen = !_isOpen;
    }

    private void Update()
    {
        if (_isOpen && !_audioSource.isPlaying && !_isChanging)
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
