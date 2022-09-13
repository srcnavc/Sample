using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class WeaponStation : BuildingControllerBase
{
    private static Action<WeaponStation> LevelChangeSignal;
    private static Action OnRemaininCostChanged;
    public static WeaponStation ins;
    public UnityEvent OnLevelUp;
    [SerializeField] int currentLevel = 0;
    [SerializeField] CurrencyContainer targetCurrencyContainer;
    [SerializeField] CurrencySC relatedCurrency;

    [SerializeField] TMP_Text currentWeaponDamage;
    [SerializeField] TMP_Text nextWeaponDamage;
    [SerializeField] TMP_Text nextLevelCost;
    [SerializeField] TMP_Text levelText;
    [SerializeField] static int remainingCost;
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

    public override void NextLevel()
    {
        if (currentLevel + 1 < levelData.Levels.Count)
        {
            if (targetCurrencyContainer == null)
                targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
            // bool isPayed = targetCurrencyContainer.DecreaseCurrency(
            //relatedCurrency.Id, levelData.Levels[currentLevel + 1].cost);

            //if (isPayed)
            {
                currentLevel++;
                remainingCost = GetGetNextLevelsCost;
                UpdateUI();
                PlayerPrefs.SetInt("WeaponStationLevel", currentLevel);
                OnLevelUp?.Invoke();
                LevelChangeSignal?.Invoke(this);
            }
        }
    }

    private void RecieveLevelUpSignal(WeaponStation station)
    {
        if (station != this)
        {
            currentLevel++;
            RemainingCost = GetGetNextLevelsCost;
            UpdateUI();
            OnLevelUp?.Invoke();
        }
    }
    public void OpenPanel()
    {
        UIManagerFlorist.ins.OpenPanel(UIManagerFlorist.ins.panelList[1]);
    }

    public void ClosePanels()
    {
        UIManagerFlorist.ins.CloseAllPanels();
    }

    public override void UpdateUI()
    {
        if (currentWeaponDamage != null)
            currentWeaponDamage.text = levelData.Levels[currentLevel].value.ToString();

        if (currentLevel + 1 < levelData.Levels.Count)
        {
            if (nextWeaponDamage != null)
                nextWeaponDamage.text = levelData.Levels[currentLevel + 1].value.ToString();
            if (nextLevelCost != null)
                nextLevelCost.text = RemainingCost.ToString();
        }
        else
        {
            if (nextWeaponDamage != null)
                nextWeaponDamage.text = "MAX";
            if (nextLevelCost != null)
                nextLevelCost.text = "MAX";
        }
        if (levelText != null)
            levelText.text = levelData.Levels[currentLevel].level.ToString();
    }

    private void Awake()
    {
        LevelChangeSignal += RecieveLevelUpSignal;
        OnRemaininCostChanged += UpdateUI;

        if (!PlayerPrefs.HasKey("WeaponStationLevel"))
            PlayerPrefs.SetInt("WeaponStationLevel", 1);

        currentLevel = PlayerPrefs.GetInt("WeaponStationLevel");

        if (!PlayerPrefs.HasKey("WeaponStationLevelRemaininCost"))
            PlayerPrefs.SetInt("WeaponStationLevelRemaininCost", GetGetNextLevelsCost);

        RemainingCost = PlayerPrefs.GetInt("WeaponStationLevelRemaininCost");



        UpdateUI();

        if (ins == null)
            ins = this;
    }
    private void OnDestroy()
    {
        LevelChangeSignal -= RecieveLevelUpSignal;
        OnRemaininCostChanged -= UpdateUI;
    }

    private void OnApplicationQuit()
    {
        if (PlayerPrefs.GetInt("WeaponStationLevelRemaininCost") != RemainingCost)
            PlayerPrefs.SetInt("WeaponStationLevelRemaininCost", RemainingCost);
    }
    public void EnableBuilding()
    {
        UpdateUI();
    }
}
