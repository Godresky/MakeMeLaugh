using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractableWithPlayerObject
{
    [SerializeField]
    private Vector3 _openedRotation;
    [SerializeField]
    private Vector3 _closedRotation;
    [SerializeField]
    private Transform _pivotPoint;
    [SerializeField]
    private float _speedRotation;
    [SerializeField]
    private AudioClip _soundOpening;
    [SerializeField]
    private AudioClip _soundClosing;

    private AudioSource _audioSource;

    private bool _isLocked = false;
    private bool _isOpen = false;
    private bool _isChangingRotation = false;

    public bool IsOpen { get => _isOpen; }

    private void Start()
    {
        if (_pivotPoint == null)
        {
            _pivotPoint = transform;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    private void Open(){
        _isOpen = true;
        _isChangingRotation = true;
        _audioSource.clip = _soundOpening;
        _audioSource.Play();
    }

    private void Close(){
        _isOpen = false;
        _isChangingRotation = true;
        _audioSource.clip = _soundClosing;
        _audioSource.Play();
    }

    public void Interact(){
        if (!_isLocked)
        {
            if (_isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
    }

    public void Lock()
    {
        _isLocked = true;
    }

    public void Unlock()
    {
        _isLocked = false;
    }

    private void Update()
    {
        if (_isChangingRotation)
        {
            Quaternion quaternionTarget = Quaternion.Euler(_isOpen ? _openedRotation : _closedRotation);
            _pivotPoint.rotation = Quaternion.RotateTowards(_pivotPoint.rotation, quaternionTarget, _speedRotation * Time.deltaTime);

            if (_pivotPoint.rotation == quaternionTarget)
                _isChangingRotation = false;
        }
    }
}
