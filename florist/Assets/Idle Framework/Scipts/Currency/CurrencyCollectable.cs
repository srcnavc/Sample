using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyCollectable : MonoBehaviour
{
    public CurrencySC relatedCurrency;
    public int value;

    CurrencyContainer container;

    private void OnTriggerEnter(Collider other)
    {
        container = other.transform.GetComponent<CurrencyContainer>();

        if (container != null && container.Contains(relatedCurrency.Id))
            AddToContainer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        container = collision.transform.GetComponent<CurrencyContainer>();

        if (container != null && container.Contains(relatedCurrency.Id))
            AddToContainer();
    }

    private void AddToContainer()
    {
        container.IncreaseCurrency(relatedCurrency.Id, value);
        gameObject.SetActive(false);
    }
}
