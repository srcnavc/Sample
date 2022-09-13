using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogExchangeAnimator : MonoBehaviour
{
    [Tooltip("If it's null, Player's CurrencyContainer is targeted.")]
    [SerializeField] CurrencyContainer targetContainer;

    private void Start()
    {
        if (targetContainer == null)
            targetContainer = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
    }

    public void Exchange(int count)
    {
        for (int i = 0; i < count; i++)
        {
            BackStackManager.ins.AddItem();
        }
    }

    public void TargetedExchange()
    {

    }
}
