using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private State _state;

    public static GameState Singleton;

    private Player _player;

    public State CurrentState { get => _state; }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Singleton = this;
    }

    public void SetUIState()
    {
        _state = State.InUI;

        _player.GameInUIState();
    }

    public void SetGameState()
    {
        _state = State.InGame;

        _player.GameInGameState();
    }

    public enum State
    {
        InGame,
        InUI
    }
}
