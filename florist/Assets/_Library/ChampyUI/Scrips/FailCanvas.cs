using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FailCanvas : CanvasBase
{
  bool levelFinalizeCompleted;
    public void updateLevelAndPoints()
    {
            levelFinalizeCompleted = true;
    }
    public void retry()
    {
        GameManager.Instance.RetryThisLevel();
    }

}
