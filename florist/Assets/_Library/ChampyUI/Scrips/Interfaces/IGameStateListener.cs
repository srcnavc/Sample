using System;

public interface IGameStateListener
{
    void StartListen();
    void StopListen();
    void OnGameStateChange(GameState gameState);
}