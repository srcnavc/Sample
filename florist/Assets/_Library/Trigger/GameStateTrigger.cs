using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateTrigger : MonoBehaviour,ITrigger
{
    public GameState TriggerState;
    public UnityEvent TriggerEvent;
 
    public UnityEvent _TriggerEvent => TriggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.OnGameStateChange += stateChange;
    }

    public void stateChange(GameState newState)
    {
        if (newState == TriggerState)
        {
            TriggerEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= stateChange;
    }
}
