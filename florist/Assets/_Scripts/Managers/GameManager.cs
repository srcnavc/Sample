using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager :MonoBehaviour, IUserInterfaceAdapter
{
    public InGameParameters InGameParameters;
    public GameSettings GameSettings;
    public static GameManager Instance;
    
    public int FinalizeLevel(bool levelCompleted)
    {
        return 0;
    }

    public GameSettings getGameSettings()
    {
        return GameSettings;
    }

    public InGameParameters getInGameParameters()
    {
        return InGameParameters;
    }

    public void increaseMoney(GameObject go)
    {
        //   parameters.economyPointsInGameNormal+=100;
    }

    public void increaseProgress(float value)
    {
        //    parameters.inGameProgress += value/100;
    }


    public LeaderBoardRecord[] getLeaderBoard()
    {
        throw new NotImplementedException();
    }

    public MultipleProgressPointData[] getMultipleProgress()
    {
        throw new NotImplementedException();
    }

   /* public ProgressInfo getProgressInfo()
    {
        return new ProgressInfo(.5f, 1f, progressCallBack);
    }*/

    public void progressCallBack()
    {
    }

    public ThreeStarsProgressData getThreeStartsWinProgress()
    {
        return new ThreeStarsProgressData(0f, .25f, .25f, .25f);
    }

    public void ProceedToNextLevel()
    {
        //  SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void RetryThisLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartLevel()
    {
        if (GameStateManager.GetState() == GameState.StartScreen)
            GameStateManager.SetState(GameState.play);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.SetState(GameState.StartScreen);
        Application.targetFrameRate = 50;
        QualitySettings.vSyncCount = 0;
        Screen.sleepTimeout = 0;
    }


    private void Awake()
    {
        Instance = this;
        InGameParameters.resetParameters();
    }
}