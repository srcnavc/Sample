using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCanvas : CanvasBase
{
  /*  public bool levelFinalizeCompleted;
    int finalPointsCollected;
    public TextMeshProUGUI FinalPoints;


    public void Initialize()
    {
        
        GetComponent<Canvas>().worldCamera = Camera.main;
        GetComponent<Canvas>().planeDistance = 1;

      
    }
    public void checkVisible(GameState state)
    {
        if (state == GameState.fail && !UIMain.getAdapter().getGameSettings().ShowMidPannelOnLoose)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void OnGameStateChanged(GameState state)
    {

        //  Debug.Log("State Event "+state);
        if (state == GameState.win || state == GameState.fail)
        {


            checkVisible(state);
            if (!levelFinalizeCompleted)
                updateLevelAndPoints();

           
        }

    }

    public void updateLevelAndPoints()
    {
        finalPointsCollected = UIMain.getAdapter().FinalizeLevel(GameStateManager.GetState() == GameState.win);
        levelFinalizeCompleted = true;
        FinalPoints.text = "+" + finalPointsCollected;
    }


    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChanged;
    }
  */
}
