using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  IUserInterfaceAdapter
{
    GameSettings getGameSettings();
    InGameParameters getInGameParameters();


    void ProceedToNextLevel();
    void RetryThisLevel();
    void StartLevel();
    int FinalizeLevel(bool levelCompleted);

    MultipleProgressPointData[] getMultipleProgress();
    ThreeStarsProgressData getThreeStartsWinProgress();
    LeaderBoardRecord[] getLeaderBoard();

  //  ProgressInfo getProgressInfo();





}
