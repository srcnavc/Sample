using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<GameType> OnGameTypeChanged;
    public static Action<PlayerState> OnPlayerStateChanged;
    [SerializeField] GameType gameType;
    [SerializeField] PlayerState state;
    
    public GameType GameType
    {
        get => gameType;
        set
        {
            if (gameType != value)
            {
                gameType = value;
                OnGameTypeChanged?.Invoke(gameType);
            }
            
        }
    }

    UIManagerFlorist UIManager => UIManagerFlorist.ins;

    public PlayerState State
    {
        get => state;

        set
        {
            if(state != value)
            {
                state = value;
                OnPlayerStateChanged?.Invoke(state);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
public enum PlayerState
{
    Idle,
    Run
}
public enum GameType
{
    Idle,
    Runner
}
