using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingTransaction : MonoBehaviour
{
    public UnityEvent OnTransactionCompleted;
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform parentTransform;
    [SerializeField] float exchangeSpeed;
    [SerializeField] float startScale = 1f;
    [SerializeField] float endScale = 1f;
    [SerializeField] bool isPlayerInExchangeArea;
    [SerializeField] bool dontUseCurrencyForStorage;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] PoolInfo stackPoolInfo;
    //[SerializeField] int cost;
    
    GameObject tempGO;
    Stack tempStack;
    IFlyingTransactionBrigde brigde;
    int counter = 0;
    float calculatedExcSpeed;
    private void Awake()
    {
        brigde = GetComponent<IFlyingTransactionBrigde>();
    }

    public bool AddItemToPile
    {
        get => isPlayerInExchangeArea; set
        {
            isPlayerInExchangeArea = value;
            if (!isPlayerInExchangeArea)
                StopCoroutine(AddItemToPileFromPlayer());
            else
                StartCoroutine(AddItemToPileFromPlayer());
        }
    }
    public float startTime;
    public float endTime;
    public void StartTransfer()
    {
        if (brigde == null)
            brigde = GetComponent<IFlyingTransactionBrigde>();

        relatedCurrency = brigde.Currency;
        CalculateExcSpeed();

        //It seems that casting as a float is redundant but it is not. It returns integer
        //if not casting as a float.
        FillImage.ins.Fill(1f - ((float)brigde.RemainingCost / (float)brigde.TotalCost));
        FillImage.ins.Activate();

        AddItemToPile = true;
        startTime = Time.time;
    }

    int forCount;
    private void CalculateExcSpeed()
    {
        calculatedExcSpeed = ((float)exchangeSpeed / (float)brigde.RemainingCost) * ((float)brigde.RemainingCost / (float)brigde.TotalCost);
        //Debug.Log("Calculated speed : " + calculatedExcSpeed);
        if (calculatedExcSpeed <= 0.1f)
        {
            float tempfloat = calculatedExcSpeed / 0.1f;
            tempfloat = 1 / tempfloat;
            forCount = (int)tempfloat;
            calculatedExcSpeed = 0.1f;
        }
        else
        {
            forCount = 1;
        }

        //Debug.Log("for Count : " + forCount);
        //Debug.Log("New Calculated speed : " + calculatedExcSpeed);
    }

    public void StopTransfer()
    {
        FillImage.ins.Deactivate();
        AddItemToPile = false;
    }


    /* private IEnumerator AddItemToPileFromPlayer()
     {
         CurrencyContainer tempCur = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();

         while (AddItemToPile && tempCur.GetCurrencyValue(relatedCurrency.Id) > 0 && brigde.RemainingCost > 0 && BackStackUp.ins.CurrentStackCount > 0)
         {
             if (!dontUseCurrencyForStorage)
                 relatedCurrency.Value++;

             //tempCur.DecreaseCurrency(relatedCurrency.Id, 1);

             tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
             tempGO.transform.parent = parentTransform;

             if (BackStackUp.ins.GetLastStackItem() != null)
                 tempGO.transform.localPosition = parentTransform.InverseTransformPoint(BackStackUp.ins.GetLastStackItem().transform.position);
             else
                 tempGO.transform.localPosition = parentTransform.InverseTransformPoint(tempCur.transform.position);

             tempGO.transform.localRotation = targetTransform.localRotation;
             tempStack = tempGO.GetComponent<Stack>();

             tempStack.TargetLocalPosition = parentTransform.InverseTransformPoint(targetTransform.position);

             tempGO.GetComponent<ReturnToPool>().IsActive = true;

             tempStack.StartMovingWithScaling(Vector3.one * startScale, Vector3.one * endScale);
             tempGO.SetActive(true);

             BackStackUp.ins.RemoveItem();
             counter++;
             brigde.RemainingCost--;

             // It seems that casting as a float is redundant but it is not.It returns integer
             //if not casting as a float.
             FillImage.ins.Fill(1f - ((float)brigde.RemainingCost / (float)brigde.TotalCost));

             yield return new WaitForSeconds(calculatedExcSpeed);
         }

         if (brigde.RemainingCost <= 0)
         {
             counter = 0;
             StopTransfer();
             OnTransactionCompleted?.Invoke();
         }
     }*/
    
    private IEnumerator AddItemToPileFromPlayer()
    {
        CurrencyContainer tempCur = GameObject.FindGameObjectWithTag("Player").GetComponent<CurrencyContainer>();
        
        while (AddItemToPile && tempCur.GetCurrencyValue(relatedCurrency.Id) > 0 && brigde.RemainingCost > 0)
        {
            for (int i = 0; i < forCount && AddItemToPile && tempCur.GetCurrencyValue(relatedCurrency.Id) > 0 && brigde.RemainingCost > 0; i++)
            {
                if (!dontUseCurrencyForStorage)
                    relatedCurrency.Value++;

                

                tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
                tempGO.transform.parent = parentTransform;


                tempGO.transform.localPosition = parentTransform.InverseTransformPoint(tempCur.transform.position);

                tempGO.transform.localRotation = targetTransform.localRotation;
                tempStack = tempGO.GetComponent<Stack>();

                tempStack.TargetLocalPosition = parentTransform.InverseTransformPoint(targetTransform.position);

                tempGO.GetComponent<ReturnToPool>().IsActive = true;

                tempStack.StartMovingWithScaling(Vector3.one * startScale, Vector3.one * endScale);
                tempGO.SetActive(true);

                tempCur.DecreaseCurrency(relatedCurrency.Id, 1);
                counter++;
                brigde.RemainingCost--;

                // It seems that casting as a float is redundant but it is not.It returns integer
                //if not casting as a float.
                FillImage.ins.Fill(1f - ((float)brigde.RemainingCost / (float)brigde.TotalCost));
            }
            
            
            yield return new WaitForSeconds(calculatedExcSpeed);
        }

        if(brigde.RemainingCost <= 0)
        {
            counter = 0;
            StopTransfer();
            OnTransactionCompleted?.Invoke();
            endTime = Time.time;
        }
    }
}

public interface IFlyingTransactionBrigde
{
    int RemainingCost { get; set; }
    int TotalCost { get; }
    CurrencySC Currency { get; }

}
