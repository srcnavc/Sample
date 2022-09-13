using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class BuildingController : BuildingControllerBase
{
    private static bool isInitilized = false;
    public static BuildingController ins;
    public static Action OnRemaininCostChanged;
    public UnityEvent OnLevelUp;
    public UnityEvent OnBuyBuilding;
    [Tooltip("In Game Cycle Time can't exceed 60 as minute.")]
    [SerializeField] int inGameCycleTime;
    [Tooltip("Idle Cycle Time can't exceed 24 as hour.")]
    [SerializeField] int idleCycleTime;
    [SerializeField] int currentLevel = 0;
    [SerializeField] CurrencyGenerator generator;
    [SerializeField] CurrencyContainer targetCurrencyContainer;

    [SerializeField] TMP_Text currentCountText;
    [SerializeField] TMP_Text nextCountText;
    [SerializeField] TMP_Text levelText;
    [SerializeField] static int remainingCost;
    [SerializeField] List<GameObject> itemsToDisableIfBuildingDisabled = new List<GameObject>();
    [SerializeField] GameObject buyColliderGo;
    public override int CurrentLevel
    {
        get => levelData.Levels[currentLevel].level;

        set
        {
            if (currentLevel != value && value < levelData.Levels.Count)
            {
                currentLevel = value;
                
                UpdateUI();
            }
        }
    }

    public override int GetGetNextLevelsCost => levelData.Levels[currentLevel + 1].cost;

    private static void SetRemainingCost(int remain)
    {
        remainingCost = remain;
        OnRemaininCostChanged?.Invoke();
    }
    public override int RemainingCost
    {
        get => remainingCost;
        set => SetRemainingCost(value);
    }
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void DisableBuilding()
    {
        GetComponent<CurrencyGenerator>().enabled = false;
        
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(false);
        
        buyColliderGo.SetActive(true);
    }

    private void EnableBuilding()
    {
        buyColliderGo.SetActive(false);
        GetComponent<CurrencyGenerator>().enabled = true;
        // Multiply by 60 for minute
        generator.timeToGenerate = FirstRemainingTime() * 60;
        
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(true);
    }

    public void Buy()
    {
        bool isPaid = targetCurrencyContainer.DecreaseCurrency(
            generator.currencyToGenerate.Id, levelData.Levels[currentLevel + 1].cost);

        if (isPaid)
        {
            PlayerPrefs.SetInt(Mayor.Stand, 1);
            EnableBuilding();
            OnBuyBuilding?.Invoke();
        }
    }

    private void Start()
    {
        generator.OnGenerate += OnCurrencyGenerate;
        OnRemaininCostChanged += UpdateUI;

        if (PlayerPrefs.GetInt(Mayor.Stand) == 0)
            DisableBuilding();
        else
        {
            EnableBuilding();
            if (!isInitilized)
            {
                isInitilized = true;
                generator.currencyToGenerate.Value += HowManyCyclesPassed();
            }
        }

        
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
        currentCountText = UIManagerFlorist.ins.panelList[0].GetComponent<UIText>().texts[0];
        nextCountText = UIManagerFlorist.ins.panelList[0].GetComponent<UIText>().texts[1];
        UpdateUI();
    }
    private void OnDestroy()
    {
        OnRemaininCostChanged -= UpdateUI;
    }
    private void OnCurrencyGenerate()
    {
        if (generator.timeToGenerate != inGameCycleTime)
        {
            // Multiply by 60 for minute
            generator.timeToGenerate = inGameCycleTime * 60;
            generator.OnGenerate -= OnCurrencyGenerate;
        }
    }

    private int FirstRemainingTime()
    {
        string str = System.DateTime.UtcNow.ToLocalTime().ToString();
        string[] subStr = str.Split(' ');
        string[] time = subStr[1].Split(':');

        int minute = int.Parse(time[1]);
        
        if (minute < inGameCycleTime)
            return inGameCycleTime - minute;
        else
            return inGameCycleTime - (minute % inGameCycleTime);
    }

    private int HowManyCyclesPassed()
    {
        return (int)(SaveManager.ins.GetPassedTime(TimeReturnType.Hours) / idleCycleTime);
    }

    public void OpenPanel()
    {
        UIManagerFlorist.ins.OpenPanel(UIManagerFlorist.ins.panelList[0]);
    }

    public void ClosePanels()
    {
        UIManagerFlorist.ins.CloseAllPanels();
    }

    public override void UpdateUI()
    {
        currentCountText.text = generator.generateAmount.ToString();

        if (currentLevel + 1 < levelData.Levels.Count)
            nextCountText.text = (levelData.Levels[currentLevel + 1].value).ToString();
        else
            nextCountText.text = "MAX";
        
        levelText.text = levelData.Levels[currentLevel].level.ToString();
    }

    public override void NextLevel()
    {
        bool isPaid = targetCurrencyContainer.DecreaseCurrency(
            generator.currencyToGenerate.Id, levelData.Levels[currentLevel + 1].cost);
        
        if (isPaid)
        {
            currentLevel++;
            generator.generateAmount = levelData.Levels[currentLevel].value;
            UpdateUI();
            OnLevelUp?.Invoke();
        }

        
    }

    private void OnValidate()
    {
        if (inGameCycleTime > 60)
            inGameCycleTime = 60;
        else if (inGameCycleTime <= 0)
            inGameCycleTime = 1;

        if (idleCycleTime > 24)
            idleCycleTime = 24;
        else if (idleCycleTime <= 0)
            idleCycleTime = 1;
    }
}
