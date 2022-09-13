using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsLevelUpPaid : MonoBehaviour, IFlyingTransactionBrigde
{
    [SerializeField] UnityEvent CanPayEvent;
    [SerializeField] UnityEvent CantPayEvent;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] BuildingControllerBase controller;
    CurrencyContainer targetCurrencyContainer;

    public int RemainingCost { get => controller.RemainingCost; set => controller.RemainingCost = value; }

    public int TotalCost => controller.GetGetNextLevelsCost;

    public CurrencySC Currency => relatedCurrency;

    private void Start()
    {
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
    }

    public void CanPay()
    {
        bool canPay = targetCurrencyContainer.GetCurrencyValue(relatedCurrency.Id) >= controller.GetGetNextLevelsCost;
        
        if (canPay)
            CanPayEvent?.Invoke();
        else
            CantPayEvent?.Invoke();
    }
}
