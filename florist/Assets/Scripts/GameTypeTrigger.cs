using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTypeTrigger : MonoBehaviour
{
    [SerializeField] GameType type;
    [SerializeField] UnityEvent OnTypeChanged;

    private void Start()
    {
        PlayerController.OnGameTypeChanged += OnGameTypeChanged;
    }

    private void OnGameTypeChanged(GameType type)
    {
        if(this.type == type)
            OnTypeChanged?.Invoke();
    }
}
