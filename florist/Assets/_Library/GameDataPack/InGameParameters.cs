using System;
using UnityEngine;

//InGameParameters sınıfı, oyun içerisinde kullanılan
//ve oyun sırasında değişen paremetreleri sahne elemanlarının
//ortak kullanmasını sağlayan sınıftır.

public abstract class InGameParameters : ScriptableObject
{
    public event Action<int> OnPlayerLevelUpdated;
    public event Action<int> OnEconomyPointsNormalUpdated;
    public event Action<int> OnEconomyPointsSpecialUpdated;
    public event Action<int> OnEconomyPointsNormalInGameUpdated;
    public event Action<int> OnEconomyPointsSpecialInGameUpdated;
    public event Action<float> OnIngameProgressUpdated;
    public ScriptableParameterList paramList;

    public int economyPointsNormal
    {
        get { return PlayerPrefs.GetInt(GameSettings.PARAM_PLAYER_POINTS_NORMAL, 0); }
        set
        {
            PlayerPrefs.SetInt(GameSettings.PARAM_PLAYER_POINTS_NORMAL, value);
            if (OnEconomyPointsNormalUpdated != null)
                OnEconomyPointsNormalUpdated.Invoke(value);
        }
    }

    public int economyPointsSpecial
    {
        get { return PlayerPrefs.GetInt(GameSettings.PARAM_PLAYER_POINTS_SPECIAL, 0); }
        set
        {
            PlayerPrefs.SetInt(GameSettings.PARAM_PLAYER_POINTS_SPECIAL, value);
            if (OnEconomyPointsSpecialUpdated != null)
                OnEconomyPointsSpecialUpdated.Invoke(value);
        }
    }

    int _economyPointsInGameNormal;

    public int economyPointsInGameNormal
    {
        get { return _economyPointsInGameNormal; }
        set
        {
            _economyPointsInGameNormal = value;
            if (OnEconomyPointsNormalInGameUpdated != null)
                OnEconomyPointsNormalInGameUpdated.Invoke(value);
        }
    }

    int _economyPointsInGameSpecial;

    public int economyPointsInGameSpecial
    {
        get { return _economyPointsInGameSpecial; }
        set
        {
            _economyPointsInGameSpecial = value;
            if (OnEconomyPointsSpecialInGameUpdated != null)
                OnEconomyPointsSpecialInGameUpdated.Invoke(value);
        }
    }


    public int playerLevel
    {
        set
        {
            PlayerPrefs.SetInt(GameSettings.PARAM_PLAYER_LEVEL_NAME, value);
            if (OnPlayerLevelUpdated != null)
                OnPlayerLevelUpdated.Invoke(value);
        }
        get { return PlayerPrefs.GetInt(GameSettings.PARAM_PLAYER_LEVEL_NAME, 0); }
    }

    public float inGameProgress
    {
        get { return _inGameProgress; }
        set
        {
            _inGameProgress = value;
            if (OnIngameProgressUpdated != null)
            {
                OnIngameProgressUpdated.Invoke(value);
            }
        }
    }

    public float _inGameProgress;

    public virtual void resetParameters()
    {
       // state = GameState.StartScreen;
        inGameProgress = 0;
        economyPointsInGameNormal = 0;
        economyPointsInGameSpecial = 0;
    }
}

[Serializable]
public enum GameState
{
    StartScreen = 0, // Very first State  (Tap To start)
    Loading = 1, // loadingSplash
    Launching = 2, //Game play start to Controls are available period (character wake up, plane takes off ect.)
    play = 3, // controls are available and game has started

    failCalculating =
        4, // Game ended player lose , waiting for calculation or adscene or any other penalty to end (untill retry enabled)
    fail = 5, // Game ended and player lose state untill retry pressed

    winCalculating =
        6, //  Game ended player won , waiting for calculation or adscene or anyother penalty to end (untill Next Level enabled)
    win = 7, // player won state (untill the retry pressed)
    Tutorial = 8, // Tutorial info
}