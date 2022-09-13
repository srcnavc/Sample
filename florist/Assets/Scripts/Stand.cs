using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public static List<Stand> ActiveStands = new List<Stand>();
    public CurrencySC relatedCurrency;
    [SerializeField] LogPile flowerStack;
    [SerializeField] BackStackUp moneyStack;
    [SerializeField] UpdateInGameUI updateInGameUI;

    FlowerTypeSC flowerTypeSC;
    public bool IsEmpty => !(relatedCurrency.Value > 0);
    public string PrefId => GetComponent<BuyProperty>().PrefId;
    private void Awake()
    {
        updateInGameUI.relatedCurrency = relatedCurrency;
    }
    public void AddActiveStandList(Stand stand)
    {
        if (!ActiveStands.Contains(stand))
            ActiveStands.Add(stand);
        else
            Debug.Log(gameObject.name + " is already in the ActiveStands list.");
    }

    public bool TakeFlower(FrontStackUp frontStackUp)
    {
        flowerTypeSC = flowerStack.AddItemToCustomer(frontStackUp);

        return flowerTypeSC != null;
    }
    public bool BuyFlower(FrontStackUp frontStackUp)
    {
        flowerTypeSC = flowerStack.AddItemToCustomer(frontStackUp);
        if (flowerTypeSC != null)
        {
            for (int i = 0; i < flowerTypeSC.SellPrice; i++)
                RegisterController.ins.AddWithScaling(transform.position, Vector3.one, Vector3.one);//moneyStack.AddItemWithScaling(transform.position, Vector3.one, Vector3.one);

            return true;
        }

        return false;
    }
}
