using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class StandController : MonoBehaviour, ICanBought
{
    public static Action OnRemaininCostChanged;
    public UnityEvent OnBuyBuilding;
    [SerializeField] string standId;
    [SerializeField] NavMeshObstacle navMeshObstacle;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] int buyCost;
    CurrencyContainer targetCurrencyContainer;
    [SerializeField] GameObject buyColliderGo;
    [SerializeField] List<GameObject> itemsToDisableIfBuildingDisabled = new List<GameObject>();
    [SerializeField] int remainingCost;
    public string PrefId => standId + "_Stand";
    public int Cost => buyCost;

    public CurrencySC RelatedCurrency => relatedCurrency;

    public int RemainingCost { get => remainingCost; set => SetRemainingCost(value); }

    private void SetRemainingCost(int remain)
    {
        remainingCost = remain;
        OnRemaininCostChanged?.Invoke();
    }
    private void DisableBuilding()
    {
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(false);

        buyColliderGo.SetActive(true);
    }

    private void EnableBuilding()
    {
        navMeshObstacle.enabled = true;
        buyColliderGo.SetActive(false);
        
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(true);
    }

    public void Buy()
    {
        bool isPaid = targetCurrencyContainer.DecreaseCurrency(
            relatedCurrency.Id, buyCost);

        if (isPaid)
        {
            PlayerPrefs.SetInt(Mayor.Stand, 1);
            EnableBuilding();
            OnBuyBuilding?.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();

        if (PlayerPrefs.GetInt(Mayor.Stand) == 0)
            DisableBuilding();
        else
            EnableBuilding();
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PrefId + "_RemaininCost"))
            PlayerPrefs.SetInt(PrefId + "_RemaininCost", Cost);

        RemainingCost = PlayerPrefs.GetInt(PrefId + "_RemaininCost");
    }
}
