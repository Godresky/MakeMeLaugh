using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

//[RequireComponent(typeof(UI))]
public class PlayerKeyboard : MonoBehaviour
{
    public Player Player;
    public ClockUI Clock;
    
    private PlayerControls _playerControls;

    private Vector2 _movementInput;
    private Vector2 _mouseInput;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();

        _playerControls = new PlayerControls();
        _playerControls.Enable();

        _playerControls.FPSControl.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _playerControls.FPSControl.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
        _playerControls.FPSControl.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();

        _playerControls.FPSControl.Crouch.performed += ctx => Player.SwitchCrouching();

        _playerControls.Actions.Clock.performed += ctx => Clock.SwitchActive();

        _playerControls.Actions.Action.performed += ctx => Player.RecievePickUp();
        _playerControls.Actions.Interact.performed += ctx => Player.RecieveInteract();
        //_playerControls.Actions.Drop.performed += ctx => Player.DropItem();

        //_playerControls.Menu.SwitchPad.performed += ctx => Stats.singleton.SwitchPadMenu();
        _playerControls.Menu.Exit.performed += ctx => Application.Quit();
        //_playerControls.Menu.Restart.performed += ctx => _level.Restart();

    }

    private void Update()
    {
        Player.RecieveInputMovement(_movementInput);
        Player.RecieveInputMouse(_mouseInput);
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
