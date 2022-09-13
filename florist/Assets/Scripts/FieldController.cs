using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;
using TMPro;

public class FieldController : BuildingControllerBase
{
    public UnityEvent OnLevelUp;
    public static Action<FieldController> OnRemaininCostChanged;
    public static Action<FieldController> LevelChangeSignal;
    [SerializeField] Transform seedContainer;
    [SerializeField] float bloomTimeInSecond = 1f;
    [SerializeField] List<SeedController> seeds = new List<SeedController>();
    [SerializeField] int currentLevel;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] int remainingCost;
    public string fieldId;
    [SerializeField] TMP_Text upgradeCostText;
    [SerializeField] TMP_Text levelText;
    public override int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public override int GetGetNextLevelsCost => levelData.Levels[currentLevel + 1].cost;
    public override int RemainingCost { get => remainingCost; set => SetRemainingCost(value); }
    public string PrefId => fieldId + "_Field";
    public float BloomTimeInSecond
    {
        get => bloomTimeInSecond;
        set
        {
            bloomTimeInSecond = value;
            UpdateBloomTime();
        }
    }
    private void SetRemainingCost(int remain)
    {
        remainingCost = remain;
        OnRemaininCostChanged?.Invoke(this);
    }
    
    private void Awake()
    {
        LevelChangeSignal += RecieveLevelUpSignal;
        OnRemaininCostChanged += RemainingCostChanged;

        if (!PlayerPrefs.HasKey(PrefId + "_Level"))
            PlayerPrefs.SetInt(PrefId + "_Level", 0);

        currentLevel = PlayerPrefs.GetInt(PrefId + "_Level");
        
        if (!PlayerPrefs.HasKey(PrefId + "_RemaininCost"))
            PlayerPrefs.SetInt(PrefId + "_RemaininCost", GetGetNextLevelsCost);

        RemainingCost = PlayerPrefs.GetInt(PrefId + "_RemaininCost");

        UpdateUI();
    }
    private void RecieveLevelUpSignal(FieldController field)
    {
        if (PrefId == field.PrefId && field != this)
        {
            currentLevel++;
            remainingCost = GetGetNextLevelsCost;
            UpdateUI();
            OnLevelUp?.Invoke();
        }
    }

    private void RemainingCostChanged(FieldController field)
    {
        if (PrefId == field.PrefId && field != this)
            remainingCost = field.RemainingCost;
        
        UpdateUI();
    }

    // Start is called before the first frame update
    void Start()
    {
        seeds = seedContainer.GetComponentsInChildren<SeedController>(true).ToList();
        bloomTimeInSecond = levelData.Levels[currentLevel].value;
        if (seeds.Count > 0)
        {
            for (int i = 0; i < seeds.Count; i++)
            {
                if (PlayerPrefs.HasKey(seeds[i].PrefId))
                {
                    seeds[i].BloomTimeInSecond = BloomTimeInSecond;
                    seeds[i].IsActive = IntToBool(PlayerPrefs.GetInt(seeds[i].PrefId));
                    
                    if (IntToBool(PlayerPrefs.GetInt(seeds[i].PrefId)))
                        seeds[i].StartGrowning();
                }
                else
                {
                    seeds[i].GetComponent<BuyProperty>().DisableBuilding();
                    seeds[i].BloomTimeInSecond = BloomTimeInSecond;

                    Debug.Log(seeds[i].gameObject.name + " has no PlayerPrefs record.");
                }
            }
        }
    }

    public void UpdateBloomTime()
    {
        
        for (int i = 0; i < seeds.Count; i++)
            seeds[i].BloomTimeInSecond = BloomTimeInSecond;
            
    }

    private bool IntToBool(int i)
    {
        if (i == 1)
            return true;
        else if (i == 0)
            return false;
        else
            throw new Exception("Invalid integer.");
    }

    public override void UpdateUI()
    {
        if (upgradeCostText != null)
            upgradeCostText.text = RemainingCost.ToString();

        if (levelText != null)
            levelText.text = (CurrentLevel + 1).ToString();
    }

    public override void NextLevel()
    {
        //bool isPaid = targetCurrencyContainer.DecreaseCurrency(
          //  relatedCurrency.Id, levelData.Levels[currentLevel + 1].cost);

        //if (isPaid)
        {
            currentLevel++;
            BloomTimeInSecond = levelData.Levels[currentLevel].value;
            RemainingCost = GetGetNextLevelsCost;
            PlayerPrefs.SetInt(PrefId + "_Level", currentLevel);
            UpdateUI();
            OnLevelUp?.Invoke();
            LevelChangeSignal?.Invoke(this);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PrefId + "_Level", currentLevel);
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", remainingCost);

    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt(PrefId + "_Level", currentLevel);
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", remainingCost);
    }
}
