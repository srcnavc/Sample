using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GameSettings Scriptable sınıfı oyunun genel davranışını
//belirleyen değişkenleri tutan ve sahne objelerinin
//bu değişkenleri ortak kullanmasını sağlayan sınıftır

public abstract class GameSettings : ScriptableObject
{
    public bool debugMode = false;
    public static string PARAM_PLAYER_LEVEL_NAME = "PlayerLevel";
    public static string PARAM_PLAYER_CHARACTER_INDEX_NAME = "PlayerCharacter";
    public static string PARAM_PLAYER_POINTS_NORMAL = "PlayerPointsNormal";
    public static string PARAM_PLAYER_POINTS_SPECIAL = "PlayerPointsSpecial";

    [HideInInspector]public ProgressBarType progressBarType;
    [HideInInspector]public DragInfoPannelType DragType;
    [HideInInspector]public WinCanvasPannelType WinLooseType;
    [HideInInspector] public bool ShowMidPannelOnLoose;
    [HideInInspector] public bool ShowLevelNumbers;


}
