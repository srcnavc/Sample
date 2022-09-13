using UnityEngine;

public class Test : MonoBehaviour
{
    public GameState gameState;
    public GameState oldState;


    private void Update()
    {
        if (gameState != oldState)
        {
            GameStateManager.SetState(gameState);
            oldState = gameState;
        }
    }
}