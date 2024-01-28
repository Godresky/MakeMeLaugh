using Unity.VisualScripting;
using UnityEngine;

//[RequireComponent(typeof(UI))]
public class PlayerKeyboard : MonoBehaviour
{
    public Player Player;
    public ClockUI Clock;
    public PauseMenuUI PauseMenu;
    public OrderPaperUI Order;
    
    private PlayerControls _playerControls;

    private Vector2 _movementInput;
    private Vector2 _mouseInput;

    public static PlayerKeyboard Singleton;

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        Clock = FindObjectOfType<ClockUI>();
        PauseMenu = FindObjectOfType<PauseMenuUI>();
        Order = FindObjectOfType<OrderPaperUI>();

        Singleton = this;

        _playerControls = new PlayerControls();
        _playerControls.Enable();

        _playerControls.FPSControl.Movement.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
        _playerControls.FPSControl.MouseX.performed += ctx => _mouseInput.x = ctx.ReadValue<float>();
        _playerControls.FPSControl.MouseY.performed += ctx => _mouseInput.y = ctx.ReadValue<float>();

        _playerControls.FPSControl.Crouch.performed += ctx => Player.SwitchCrouching();

        _playerControls.Actions.Action.performed += ctx => Player.RecievePickUp();
        _playerControls.Actions.Interact.performed += ctx => Player.RecieveInteract();
        _playerControls.Actions.Clock.performed += ctx => Clock.SwitchActive();
        _playerControls.Actions.OrderPaper.performed += ctx => Order.SwitchActive();
        //_playerControls.Actions.Drop.performed += ctx => Player.DropItem();

        //_playerControls.Menu.SwitchPad.performed += ctx => Stats.singleton.SwitchPadMenu();
        //_playerControls.Menu.Exit.performed += ctx => Application.Quit();
        _playerControls.Menu.Exit.performed += ctx => PauseMenu.Switch();
        //_playerControls.Menu.Restart.performed += ctx => _level.Restart();

    }

    private void Update()
    {
        if (GameState.Singleton.CurrentState == GameState.State.InGame)
        {
            Player.RecieveInputMovement(_movementInput);
            Player.RecieveInputMouse(_mouseInput);
        }
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
