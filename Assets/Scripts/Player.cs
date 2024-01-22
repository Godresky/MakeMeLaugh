using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
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
    private float _crouchingCoefficient = 0.2f;

    [SerializeField]
    private bool _isCrouching = false;

    [SerializeField]
    private CharacterController _controller;
    private float _playerHeight;

    [Header("Camera Settings")]
    [SerializeField]
    private Vector2 _sensetivity = new Vector2(8f, 0.5f);
    [SerializeField]
    private float _xClamp;

    [SerializeField]
    private Transform _cameraTransform;

    private float _xRotation = 0f;
    private Vector2 _mouseInput;

    private Vector2 _movementInput;
    private Vector2 _verticalVelocity = Vector2.zero;
    private Vector3 _teleportPosition = Vector3.zero;

    private Camera _camera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _controller = GetComponent<CharacterController>();
        _playerHeight = _controller.height;

        _camera = _cameraTransform.GetComponent<Camera>();
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

    public void SwitchCrouching()
    {
        _isCrouching = !_isCrouching;
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

    //private void CheckAudio(float speed)
    //{
    //    if (_controller.isGrounded && speed > 2f && !_audio.isPlaying && _canPlayAudio)
    //    {
    //        StartCoroutine(Footstep());
    //        _canPlayAudio = false;
    //    }
    //}

    private void Movement()
    {
        // WASD
        if (_controller.isGrounded)
            _verticalVelocity.y = 0;

        Vector3 horizontalVelocity = (transform.right * _movementInput.x + transform.forward * _movementInput.y) * (_isCrouching ? _speedCrouching : _speedWalking);
        _controller.Move(horizontalVelocity * Time.deltaTime);

        _verticalVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(_verticalVelocity * Time.deltaTime);

        // Crouching
        if (_isCrouching && _controller.height > (_playerHeight / 2))
        {
            _controller.height -= _crouchingCoefficient;
        }
        else if (!_isCrouching && _controller.height < _playerHeight)
        {
            _controller.height += _crouchingCoefficient;
        }

        //CheckAudio(horizontalVelocity.magnitude);
    }

    private void Update()
    {
        MouseLook();
        Movement();

        //CheckRaycast();
    }

}
