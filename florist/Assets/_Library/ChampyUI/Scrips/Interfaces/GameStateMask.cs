using System;
using System.Collections.Generic;

[Serializable]
public class GameStateMask
{
    public List<GameState> gameStates = new List<GameState>();

    public bool GamesStateContains(GameState gameState)
    {
        return gameStates.Contains(gameState);
    }
}