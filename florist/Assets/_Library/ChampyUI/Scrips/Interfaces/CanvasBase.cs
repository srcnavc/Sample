using UnityEngine;

public abstract class CanvasBase : MonoBehaviour, IGameStateListener, IActivator
{
    public GameStateMask GameStateMask;

    protected virtual void Start()
    {
        //  ListenState();
        // 
    }

    public void StartListen()
    {
        GameStateManager.OnGameStateChange += OnGameStateChange;
        OnGameStateChange(GameStateManager.GetState());
    }

    public void StopListen()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChange;
    }

    public void OnGameStateChange(GameState gameState)
    {
        SetActivate(GameStateMask.GamesStateContains(gameState));
    }

    public void SetActivate(bool val)
    {
        // GetComponent<Canvas>().enabled = val;
        gameObject.SetActive(val);
     //   Debug.Log(name + " " + val);
    }

    protected virtual void OnDestroy()
    {
        StopListen();
    }
}