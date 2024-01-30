using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private State _state;
    [SerializeField]
    private State _previousState;

    [SerializeField]
    private AudioSource _ostGame, _ostUI, _ostEndGame;

    public static GameState Singleton;

    private bool _endGame = false;

    private Player _player;

    public State CurrentState { get => _state; }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Singleton = this;
    }

    public void SetUIState()
    {
        _previousState = _state;
        _state = State.InUI;

        _player.GameInUIState();
        _ostUI.volume = 1f;
        _ostGame.volume = 0f;
    }

    public void SetPauseState()
    {
        SetUIState();
        _state = State.InPause;
    }

    public void SetEndGameState()
    {
        if (!_endGame)
        {
            _endGame = true;
            _state = State.InUI;

            _player.GameInUIState();
            _ostUI.Stop();
            _ostGame.Stop();
            _ostEndGame.Play();
        }
    }

    public void SetGameState()
    {
        _state = State.InGame;

        _player.GameInGameState();
        _ostUI.volume = 0f;
        _ostGame.volume = 1f;
    }

    public void SetGameStateWithCheck()
    {
        if (_previousState == State.InGame) 
        {
            SetGameState();
        }
        else if (_previousState == State.InUI)
        {
            SetUIState();
        }
    }


    public enum State
    {
        InGame,
        InUI,
        InPause
    }
}
