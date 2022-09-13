using UnityEngine;

public class UIMain : MonoBehaviour
{
    //TODO  Move the UI selections to a scriptablebin
    public static UIMain instance;
    public GameObject UIAdapter;
    [HideInInspector] public bool DebugMode;
    [HideInInspector] public bool VideoMode;
   // IUserInterfaceAdapter _adapter;

    /*public PoolInfo flyingMoneyPoolInfo;
    public PoolInfo flyingSpecialMoneyPoolInfo;
    [SerializeField]public GameObject utilityCanvas,StartCanvas,LooseCanvas,InGameCanvas,WinCanvas,ScoreCanvas,FXCanvas;
    [HideInInspector]public WinCanvasObjects winCanvasObjects;
    [HideInInspector]public StartCanvasObjects startCanvasObjects;
    [HideInInspector]public IngameProgressBar progressBar;
    public GameObject MoneyFlyTarget,SpecialMoneyFlyTarget;
    public static string FlyingMoneyName
    {
        get {
            if (instance.flyingMoneyPoolInfo != null)
                return instance.flyingMoneyPoolInfo.PoolName;
            else
            {
                Debug.LogError("PoolInfo for FlyingMoney Object is missing");
                return null;
            }
        }
    }
    public static string FlyingSpecialMoneyName
    {
        get
        {
            if (instance.flyingSpecialMoneyPoolInfo != null)
                return instance.flyingSpecialMoneyPoolInfo.PoolName;
            else
            {
                Debug.LogError("PoolInfo for FlyingSpecialMoney Object is missing");
                return null;
            }
        }
    }*/
  /*  public IUserInterfaceAdapter adapter
    {
        get
        {
            if (_adapter == null)
            {
                if (UIAdapter != null)
                    _adapter = UIAdapter.GetComponent<IUserInterfaceAdapter>();
            }

            return _adapter;
        }
    }*/
    /*public static GameObject PointsFlyTarget
    {
        get
        {
            return instance.MoneyFlyTarget;
        }
    }
    public static GameObject SpecialPointsFlyTarget
    {
        get
        {
            return instance.SpecialMoneyFlyTarget;
        }
    }*/

    void Start()
    {
        /*adapter.getInGameParameters().OnGameStateChanged += onGameStateChanged;
        winCanvasObjects = WinCanvas.GetComponent<WinCanvasObjects>();
        startCanvasObjects = StartCanvas.GetComponent<StartCanvasObjects>();*/
        var listeners = GetComponentsInChildren<IGameStateListener>(true);
        for (int i = 0; i < listeners.Length; i++)
        {
            listeners[i].StartListen();
        }

        init();
        /*refresh();
        StartPannelRefresh();
        WinLoosePannelRefresh();
        createUIPools();*/
    }

    void init()
    {
        /*utilityCanvas.SetActive(DebugMode);*/
    }
    /*
    public void refresh()
    {
        utilityCanvas.SetActive(DebugMode);
        StartCanvas.SetActive(adapter.getInGameParameters().state == GameState.StartScreen);
        LooseCanvas.SetActive(adapter.getInGameParameters().state == GameState.fail);
        InGameCanvas.SetActive(adapter.getInGameParameters().state == GameState.play&&!VideoMode);
        WinCanvas.SetActive(adapter.getInGameParameters().state == GameState.win);
        ScoreCanvas.SetActive(adapter.getInGameParameters().state == GameState.win|| adapter.getInGameParameters().state == GameState.fail);
    }
    public void StartPannelRefresh()
    {
        startCanvasObjects.DragPanelXY.SetActive(adapter.getGameSettings().DragType== DragInfoPannelType.DragXY);
        startCanvasObjects.DragPanelX.SetActive(adapter.getGameSettings().DragType == DragInfoPannelType.DragX);
        startCanvasObjects.DragPannelY.SetActive(adapter.getGameSettings().DragType == DragInfoPannelType.DragY);
        startCanvasObjects.TapPannel.SetActive(adapter.getGameSettings().DragType == DragInfoPannelType.Tap);
    }
    */

    /*public void WinLoosePannelRefresh()
    {
        if (adapter.getGameSettings() == null)
        {
            Debug.Log("GameSettings is null");        
        }

        if (winCanvasObjects == null)
        {
            Debug.Log("leaderBoard is null ");
        }


        winCanvasObjects.LeaderBoardPannel.SetActive(adapter.getGameSettings().WinLooseType == WinCanvasPannelType.LeaderBoard);
        winCanvasObjects.TreeStarsPannel.SetActive(adapter.getGameSettings().WinLooseType == WinCanvasPannelType.ThreeStarts);
        winCanvasObjects.SingleProgressPannel.SetActive(adapter.getGameSettings().WinLooseType == WinCanvasPannelType.progress);
    }

    public void  onGameStateChanged(GameState state)
    {
        refresh();
    }*/
    private void Awake()
    {
        instance = this;
    }

   /* public static IUserInterfaceAdapter getAdapter()
    {
        if (instance == null)
            return null;

        return instance.adapter;
    }
   */

    /*
    public void createUIPools()
    {
        if (!PoolManager.IsCreated)
            return;

        if (flyingMoneyPoolInfo != null) 
            PoolManager.CreatePool(flyingMoneyPoolInfo);

        if (flyingSpecialMoneyPoolInfo != null)
        {
            PoolManager.CreatePool(flyingSpecialMoneyPoolInfo);
        }
        else
            Debug.Log("SpecialPoints Info is null");
    }

    /// <summary>
    /// FlyMoney static function will run a flying money tween animation on UI from given position to default Money visual position.
    /// This function only works during GameState "==" Play case , will call the CallBack delegate when the fly animation is
    /// completed
    /// </summary>
    /// <param name="StartPositionUI">StartPositionUI is the UI start position of the money(point) visual , 
    /// If youy dont have this, use the alternative call for worldposition</param>
    /// <param name="speed">Speed parameter determines the duration of the animation. 0.5f will set the animation to complete in 2 secconds </param>
    /// <param name="flyMoneyCallBack">CallBack delegate is the reference to the callback function to be invoked when the tweenAnimation is completed.
    /// Importan parameter If you want to syncronize the increment of your points with the UI animation. 
    /// Unless you can make the increment before or after calling FlyMoney </param>
    public static void FlyMoney(Vector3 StartPositionUI, float speed ,AnimationCurve curve, SimpleTranslate.CallBack flyMoneyCallBack)
    {
        GameObject FlyingMoney = PoolManager.fetch(FlyingMoneyName);

        
        SimpleTranslate st = FlyingMoney.GetComponent<SimpleTranslate>();
        if (st == null)
            st = FlyingMoney.AddComponent<SimpleTranslate>();
        st.Translate(.3f, PointsFlyTarget.transform.position, true, flyMoneyCallBack);
        FlyingMoney.transform.SetParent(_InGameCanvas.transform);
        FlyingMoney.transform.position = StartPositionUI;
        SimpleSizeTween sst = FlyingMoney.GetComponent<SimpleSizeTween>();
        if (sst == null)
            sst = FlyingMoney.AddComponent<SimpleSizeTween>();
        sst.StartAnimation(.3f, 0.3f, curve, null);

    }


    /// <summary>
    /// FlySpecialMoney static function will run a flying money tween animation on UI from given position to default SpecialMoney visual position.
    /// This function only works during GameState "==" Play case , will call the CallBack delegate when the fly animation is
    /// completed
    /// </summary>
    /// <param name="StartPositionUI">StartPositionUI is the UI start position of the money(point) visual , 
    /// If youy dont have this, use the alternative call for worldposition</param>
    /// <param name="speed">Speed parameter determines the duration of the animation. 0.5f will set the animation to complete in 2 secconds </param>
    /// <param name="flyMoneyCallBack">CallBack delegate is the reference to the callback function to be invoked when the tweenAnimation is completed.
    /// Importan parameter If you want to syncronize the increment of your points with the UI animation. 
    /// Unless you can make the increment before or after calling FlyMoney </param>
    public static void FlySpecialMoney(Vector3 StartPositionUI, float speed, AnimationCurve curve, SimpleTranslate.CallBack flyMoneyCallBack)
    {
        GameObject FlyingMoney = PoolManager.fetch(FlyingSpecialMoneyName);


        SimpleTranslate st = FlyingMoney.GetComponent<SimpleTranslate>();
        if (st == null)
            st = FlyingMoney.AddComponent<SimpleTranslate>();
        st.Translate(.3f, SpecialPointsFlyTarget.transform.position, true, flyMoneyCallBack);
        FlyingMoney.transform.SetParent(_InGameCanvas.transform);
        FlyingMoney.transform.position = StartPositionUI;
        SimpleSizeTween sst = FlyingMoney.GetComponent<SimpleSizeTween>();
        if (sst == null)
            sst = FlyingMoney.AddComponent<SimpleSizeTween>();
        sst.StartAnimation(.3f, 0.3f,curve, null);

    }


    /// <summary>
    /// FlyMoney static function will run a flying money tween animation on UI from given position to default Money visual position.
    /// This function only works during GameState "==" Play case , will call the CallBack delegate when the fly animation is
    /// completed
    /// </summary>
    /// <param name="WorldObjectForPositioning">Is the position reference object where the flyingMoney will be originated from</param>
    /// <param name="speed">Speed parameter determines the duration of the animation. 0.5f will set the animation to complete in 2 secconds </param>
    /// <param name="flyMoneyCallBack">CallBack delegate is the reference to the callback function to be invoked when the tweenAnimation is completed.
    /// Importan parameter If you want to syncronize the increment of your points with the UI animation. 
    /// Unless you can make the increment before or after calling FlyMoney </param>
    public static void FlyMoneyWithWorldPosition(Vector3 WorldObjectForPosition, float speed, AnimationCurve curve, SimpleTranslate.CallBack flyMoneyCallBack)
    {
        FlyMoney(Camera.main.WorldToScreenPoint(WorldObjectForPosition), speed, curve, flyMoneyCallBack);
    }

    /// <summary>
    /// FlySpecialMoney static function will run a flying money tween animation on UI from given position to default Money visual position.
    /// This function only works during GameState "==" Play case , will call the CallBack delegate when the fly animation is
    /// completed
    /// </summary>
    /// <param name="WorldObjectForPositioning">Is the position reference object where the flyingMoney will be originated from</param>
    /// <param name="speed">Speed parameter determines the duration of the animation. 0.5f will set the animation to complete in 2 secconds </param>
    /// <param name="flyMoneyCallBack">CallBack delegate is the reference to the callback function to be invoked when the tweenAnimation is completed.
    /// Importan parameter If you want to syncronize the increment of your points with the UI animation. 
    /// Unless you can make the increment before or after calling FlyMoney </param>
    public static void FlySpecialMoneyWithWorlPosition(Vector3 WorldObjectForPosition, float speed, AnimationCurve curve, SimpleTranslate.CallBack flyMoneyCallBack)
    {
        FlySpecialMoney(Camera.main.WorldToScreenPoint(WorldObjectForPosition), speed, curve, flyMoneyCallBack);
    }*/
}


public class ThreeStarsProgressData
{
    public float TotalProgress;
    public float FirsStarValue;
    public float SecondStarValue;
    public float ThirdStarValue;
    public float[] starValues;

    public ThreeStarsProgressData(float totalProgress, float firsStarValue, float secondStarValue, float thirdStarValue)
    {
        TotalProgress = totalProgress;
        FirsStarValue = firsStarValue;
        SecondStarValue = secondStarValue;
        ThirdStarValue = thirdStarValue;
        starValues = new float[3];
        starValues[0] = firsStarValue;
        starValues[1] = secondStarValue;
        starValues[2] = thirdStarValue;
    }
}

public class MultipleProgressPointData
{
    public float position;
    public Color color;
    public float Offsety;
}


public class LeaderBoardRecord
{
    public int SortId;
    public string UserName;
    public Color displayColor;
}




public enum WinCanvasPannelType
{
    ThreeStarts = 0,
    LeaderBoard = 1,
    progress = 2
}

public enum DragInfoPannelType
{
    DragXY = 0,
    DragX = 1,
    DragY = 2,
    Tap = 3
}

public enum ProgressBarType
{
    Fill = 0,
    Point = 1,
    Both = 2,
    Multiple = 3
}