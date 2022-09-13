using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Player : MonoBehaviour
{
    static Player Instance;
    public static Player getCurrentPlayer()
    {
        return Instance;
    }
    private void Awake()
    {
        Instance = this;
    }
}
