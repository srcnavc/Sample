using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCanvas : CanvasBase
{

    public void Initialize()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 1;
    }
    
    public void nextLevel()
    {
        GameManager.Instance.ProceedToNextLevel();
    }


}



