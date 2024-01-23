using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerPickingUp))]
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
    private float _crouchingTransitionSpeed = 10f;
    [SerializeField]
    private float _crouchHeight = 1f;
    private float _standingHeight;
    private float _currentHeight;
    private bool _isCrouching = false;
    private Vector3 _initialCameraPosition;

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

    private float _xRotation = 0f;
    private Vector2 _mouseInput;

    private Vector2 _movementInput;
    private Vector2 _verticalVelocity = Vector2.zero;
    private Vector3 _teleportPosition = Vector3.zero;

    private Camera _camera;
    private PlayerPickingUp _playerPickingUp;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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

    private void MouseLook()
    {
        transform.Rotate(Vector3.up, _mouseInput.x * Time.deltaTime);

        _xRotation -= _mouseInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -_xClamp, _xClamp);
        Vector3 targetRotation = transform.eulerAngles;

        targetRotation.x = _xRotation;
        _cameraTransform.eulerAngles = targetRotation;
    }

    public void SwitchCrouching()
    {
        _isCrouching = !_isCrouching;
    }

    public void Crouching()
    {
        // Transform Character
        var heightTarget = _isCrouching ? _crouchHeight : _standingHeight;

        var crouchDelta = Time.deltaTime * _crouchingTransitionSpeed;
        _currentHeight = Mathf.Lerp(_currentHeight, heightTarget, crouchDelta);
        //if (_isCrouching && _currentHeight > _crouchHeight)
        //{
        //    _currentHeight -= crouchDelta;
        //}
        //else if (!_isCrouching && _currentHeight < heightTarget)
        //{
        //    _currentHeight += crouchDelta;
        //}

        _controller.height = _currentHeight;

        // Tranform camera
        var halfHeightDifference = new Vector3(0, (_standingHeight - _currentHeight) / 2, 0);
        var newCameraPosition = _initialCameraPosition - halfHeightDifference;

        _cameraTransform.localPosition = newCameraPosition;
    }

    //private void CheckAudio(float speed)
    //{
    //    if (_controller.isGrounded && speed > 2f && !_audio.isPlaying && _canPlayAudio)
    //    {
    //        StartCoroutine(Footstep());
    //        _canPlayAudio = false;
    //    }
    //}

    private void CheckRaycast()
    {
        //if (_lastHoveredItem != null)
        //{
        //    _lastHoveredItem.OnHoverExit();
        //    _lastHoveredItem = null;
        //}

        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _distanceRaycast, _layerMaskRaycast, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.TryGetComponent(out PickableItem item))
            {
                //if (_lastHoveredItem != null)
                //    _lastHoveredItem.OnHoverExit();

                //_lastHoveredItem = outlineItem;
                //outlineItem.OnHoverEnter();

                if (_isPickingUp)
                {
                    _playerPickingUp.PickUp(hit.collider.gameObject);
                }
            }
        }
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

        //CheckAudio(horizontalVelocity.magnitude);
    }

    private void Update()
    {
        MouseLook();
        Movement();
        Crouching();

        CheckRaycast();
    }

}
