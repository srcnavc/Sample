using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class BuyProperty : MonoBehaviour, ICanBought
{
    private static Action<BuyProperty> BuySignal;
    private static Action<BuyProperty> OnRemaininCostChanged;
    public Action<BuyProperty> OnBuyProperty;
    public UnityEvent OnBuyBuilding;
    public string propertyId;
    public bool enabledInStart;
    //public bool IsActive;
    [SerializeField] List<GameObject> itemsToDisableIfBuildingDisabled = new List<GameObject>();
    [SerializeField] GameObject buyColliderGo;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] int cost;
    [SerializeField] int remainingCost;
    [SerializeField] TMP_Text buyCostText;
    DoPunchScale[] doPunch;
    CurrencyContainer targetCurrencyContainer;
    int tempPrefIntResult;
    public string PrefId => propertyId + "_Property";

    public int Cost => cost;
    public bool IsActive
    {
        get
        {
            if (!enabledInStart)
            {
                if (!PlayerPrefs.HasKey(PrefId))
                    PlayerPrefs.SetInt(PrefId, 0);
                else
                    tempPrefIntResult = PlayerPrefs.GetInt(PrefId);
            }
            else
            {
                tempPrefIntResult = 1;
            }
            
            
            return tempPrefIntResult == 1;
        }
    }
    public CurrencySC RelatedCurrency => relatedCurrency;
    public int RemainingCost { get => remainingCost; set => SetRemainingCost(value); }
    public GameObject BuyColliderGo { get => buyColliderGo; set => buyColliderGo = value; }

    private void SetRemainingCost(int remain)
    {
        remainingCost = remain;
        OnRemaininCostChanged?.Invoke(this);
    }
    public void DisableBuilding()
    {
        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
            itemsToDisableIfBuildingDisabled[i].SetActive(false);

        //BuyColliderGo.SetActive(true);
        //IsActive = false;
    }

    
    public void EnableBuilding()
    {
        BuyColliderGo.SetActive(false);

        for (int i = 0; i < itemsToDisableIfBuildingDisabled.Count; i++)
        {
            itemsToDisableIfBuildingDisabled[i].SetActive(true);

            // DO PUNCH

            /*doPunch = itemsToDisableIfBuildingDisabled[i].GetComponentsInChildren<DoPunchScale>();
            Debug.Log(itemsToDisableIfBuildingDisabled[i].name + " " + doPunch.Length);
            if (doPunch.Length > 0)
            {
                
                //StartCoroutine(Sirali(doPunch));
                /*for (int k = 0; k < doPunch.Length; k++)
                {
                    doPunch[k].PunchIt();
                }
            }*/

        }

        InfiniteRoad.ins.BuildNavmesh();
        //IsActive = true;
        OnBuyBuilding?.Invoke();
        OnBuyProperty?.Invoke(this);
    }
    private void RemainingCostChanged(BuyProperty property)
    {
        if (PrefId == property.PrefId && property != this)
            remainingCost = property.RemainingCost;
        
        UpdateUI();
    }
    public void Buy(bool calledByEvent)
    {
        bool isPaid;

        if (!calledByEvent)
            isPaid = targetCurrencyContainer.DecreaseCurrency(relatedCurrency.Id, Cost);
        else
            isPaid = true;

        if (isPaid)
        {
            if (!calledByEvent)
                PlayerPrefs.SetInt(PrefId, 1);

            EnableBuilding();
            

            if (!calledByEvent)
                BuySignal?.Invoke(this);
        }
        else
            Debug.Log(gameObject.name + " : not enought money.");
    }

    public void GiveOwnerShip()
    {
        PlayerPrefs.SetInt(PrefId, 1);
        EnableBuilding();
        
        BuySignal?.Invoke(this);
    }

    private void BuySignalReciever(BuyProperty signalOwner)
    {
        if (PrefId.Equals(signalOwner.PrefId) && signalOwner != this)
            Buy(true);
    }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PrefId))
        {
            if(!enabledInStart)
                PlayerPrefs.SetInt(PrefId, 0);
            else
                PlayerPrefs.SetInt(PrefId, 1);
        }

        if (!PlayerPrefs.HasKey(PrefId + "_RemaininCost"))
            PlayerPrefs.SetInt(PrefId + "_RemaininCost", Cost);

        RemainingCost = PlayerPrefs.GetInt(PrefId + "_RemaininCost");

        UpdateUI();
    }
    private void UpdateUI()
    {
        if (buyCostText != null)
            buyCostText.text = RemainingCost.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        BuySignal += BuySignalReciever;
        OnRemaininCostChanged += RemainingCostChanged;

        if (PlayerPrefs.GetInt(PrefId) == 0)
            DisableBuilding();
        else
            EnableBuilding();
        
        targetCurrencyContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
    }

    private void OnDestroy()
    {
        BuySignal -= BuySignalReciever;
        OnRemaininCostChanged -= RemainingCostChanged;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", RemainingCost);

    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", RemainingCost);
    }
}
