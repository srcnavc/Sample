using System;
using UnityEngine;

public class GameStateManager
{
    private static GameStateManager _gameStateManager;
    [SerializeField] private GameState currentState;
    public static event Action<GameState> OnGameStateChange;

    public static GameState GetState()
    {
        if (_gameStateManager == null)
        {
            _gameStateManager = new GameStateManager();
        }

        return _gameStateManager.currentState;
    }

    public static void SetState(GameState gameState)
    {
        if (_gameStateManager == null)
        {
            _gameStateManager = new GameStateManager();
        }

        _gameStateManager.currentState = gameState;
        OnGameStateChange?.Invoke(gameState);
    }
}