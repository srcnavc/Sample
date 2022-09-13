using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IsBuildingPaid : MonoBehaviour, IFlyingTransactionBrigde
{
    [SerializeField] UnityEvent CanPayEvent;
    [SerializeField] UnityEvent CantPayEvent;
    CurrencyContainer targetCurrencyContainer;
    ICanBought canBought;

    public int RemainingCost { get => canBought.RemainingCost; set => canBought.RemainingCost = value; }

    public int TotalCost => canBought.Cost;

    public CurrencySC Currency => canBought.RelatedCurrency;

    private void Start()
    {
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
        canBought = GetComponentInParent<ICanBought>();

        if (canBought == null)
            Debug.Log("There is no ICanBought in parent.");
    }

    public void CanPay()
    {
        bool canPay = targetCurrencyContainer.GetCurrencyValue(canBought.RelatedCurrency.Id) >= canBought.Cost;

        if (canPay)
            CanPayEvent?.Invoke();
        else
            CantPayEvent?.Invoke();
    }
}

public interface ICanBought
{
    int RemainingCost { get; set; }
    int Cost { get; }
    CurrencySC RelatedCurrency { get; }
}
