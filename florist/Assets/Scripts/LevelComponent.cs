using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComponent : MonoBehaviour
{
    public static Action OnPlayerLeaveIdleGround;
    public Transform parentTransform;

    public void TriggerPlayerLeaveIdleGround()
    {
        OnPlayerLeaveIdleGround?.Invoke();
    }
}
