using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerPickingUp))]
[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float _speedWalking = 5;
    [SerializeField]
    private float _speedCrouching = 2.5f;
    [SerializeField]
    private float _gravity = -30f;
    [SerializeField]
    // Crouching parameters
    private float _crouchingTransitionSpeed = 10f;
    [SerializeField]
    private float _crouchHeight = 1f;
    private float _standingHeight;
    private float _currentHeight;
    private bool _isTryingToCrouch = false;
    private Vector3 _initialCameraPosition;
    private bool _isCrouching => _standingHeight - _currentHeight > .1f;


    [SerializeField]
    private CharacterController _controller;

    [Header("Camera Settings")]
    [SerializeField]
    private Vector2 _sensetivity = new Vector2(8f, 0.5f);
    [SerializeField]
    private float _xClamp;
    [SerializeField]
    private Transform _cameraTransform;

    [Header("Pucking Up Settings")]
    [SerializeField]
    private float _distanceRaycast = Mathf.Infinity;
    [SerializeField]
    private LayerMask _layerMaskRaycast;

    private bool _isPickingUp = false;
    private bool _isInteracting = false;

    private float _xRotation = 0f;
    private Vector2 _mouseInput;

    private Vector2 _movementInput;
    private Vector2 _verticalVelocity = Vector2.zero;

    private Camera _camera;
    private PlayerPickingUp _playerPickingUp;
    private PickableItem _lastHoveredItem;

    private AudioSource _audio;

    [Header("Sound Settings")]
    [SerializeField]
    private List<AudioClip> _footstepsClips;
    [SerializeField]
    private float _delayWalk;
    [SerializeField]
    private float _delayСrouching;
    [SerializeField]
    private Vector2 _rangeFootstepsVolume = new(0.65f, 0.75f);
    [SerializeField]
    private Vector2 _rangeFootstepsPitch = new(0.8f, 1.1f);
    [SerializeField]
    private bool _canPlayAudio = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _audio = GetComponent<AudioSource>();
        _controller = GetComponent<CharacterController>();
        _playerPickingUp = GetComponent<PlayerPickingUp>();

        _standingHeight = _currentHeight = _controller.height;

        _camera = _cameraTransform.GetComponent<Camera>();
        _initialCameraPosition = _cameraTransform.localPosition;
    }

    public void RecieveInputMovement(Vector2 input)
    {
        _movementInput = input;
    }

    public void RecieveInputMouse(Vector2 input)
    {
        _mouseInput.x = input.x * _sensetivity.x;
        _mouseInput.y = input.y * _sensetivity.y;
    }

    public void RecievePickUp()
    {
        _isPickingUp = !_isPickingUp;

        if (!_isPickingUp)
        {
            _playerPickingUp.Drop();
        }
    }

    public void RecieveInteract()
    {
        _isInteracting = true;
    }

    public void SwitchCrouching()
    {
        _isTryingToCrouch = !_isTryingToCrouch;
    }

    private void MouseLook()
    {
        transform.Rotate(Vector3.up, _mouseInput.x * Time.deltaTime);

        _xRotation -= _mouseInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;

        targetRotation.x = _xRotation;
        _cameraTransform.eulerAngles = targetRotation;
    }

    public void Crouching()
    {
        // Transform Character
        var heightTarget = _isTryingToCrouch ? _crouchHeight : _standingHeight;

        if (_isCrouching && !_isTryingToCrouch)
        {
            var castOrigin = transform.position + new Vector3(0, _currentHeight / 2, 0);
            if (Physics.Raycast(castOrigin, Vector3.up, out RaycastHit hit, 0.2f))
            {
                var distanceToCeiling = hit.point.y - castOrigin.y;
                heightTarget = Mathf.Max
                (
                    _currentHeight + distanceToCeiling - 0.1f,
                    _crouchHeight
                );
            }
        }

        var crouchDelta = Time.deltaTime * _crouchingTransitionSpeed;
        _currentHeight = Mathf.Lerp(_currentHeight, heightTarget, crouchDelta);

        _controller.height = _currentHeight;

        // Tranform camera
        var halfHeightDifference = new Vector3(0, (_standingHeight - _currentHeight) / 2, 0);
        var newCameraPosition = _initialCameraPosition - halfHeightDifference;

        _cameraTransform.localPosition = newCameraPosition;
    }

    private void CheckAudio(float speed)
    {
        if (_controller.isGrounded && speed > 1f && !_audio.isPlaying && _canPlayAudio)
        {
            StartCoroutine(Footstep());
            _canPlayAudio = false;
        }
    }

    private IEnumerator Footstep()
    {
        int numberRandomClip = Random.Range(0, _footstepsClips.Count);

        _audio.clip = _footstepsClips[numberRandomClip];

        _audio.volume = Random.Range(_rangeFootstepsVolume.x, _rangeFootstepsVolume.y);
        _audio.pitch = Random.Range(_rangeFootstepsPitch.x, _rangeFootstepsPitch.y);
        _audio.Play();

        yield return new WaitForSeconds(_isCrouching ? _delayСrouching : _delayWalk);

        _canPlayAudio = true;
    }

    private void CheckRaycast()
    {
        PickableItem currentEquippedItem = _playerPickingUp.GetEquippedItem();
        if (_lastHoveredItem != null && currentEquippedItem != _lastHoveredItem)
        {
            _lastHoveredItem.OnHoverExit();
            _lastHoveredItem = null;
        }

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distanceRaycast, _layerMaskRaycast, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent(out PickableItem item))
            {
                if (currentEquippedItem == null && _lastHoveredItem != item)
                {
                    if (_lastHoveredItem != null)
                        _lastHoveredItem.OnHoverExit();

                    _lastHoveredItem = item;
                    item.OnHoverEnter();
                }

                if (_isPickingUp)
                {
                    _playerPickingUp.PickUp(item.gameObject);
                }
            }
            if (hit.collider.TryGetComponent(out IInteractableWithPlayerObject interactabelObject))
            {
                if (_isInteracting)
                {
                    _isInteracting = false;
                    interactabelObject.Interact();
                }
            }
        }

        _isInteracting = false;
    }

    private void Movement()
    {
        // WASD
        if (_controller.isGrounded)
            _verticalVelocity.y = 0;

        Vector3 horizontalVelocity = (transform.right * _movementInput.x + transform.forward * _movementInput.y) * (_isCrouching ? _speedCrouching : _speedWalking);
        _controller.Move(horizontalVelocity * Time.deltaTime);

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);

        CheckAudio(horizontalVelocity.magnitude);
    }

    private void Update()
    {
        MouseLook();
        Movement();
        Crouching();

        CheckRaycast();
    }
}
