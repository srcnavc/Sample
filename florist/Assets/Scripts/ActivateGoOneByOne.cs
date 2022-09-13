using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateGoOneByOne : MonoBehaviour
{
    [SerializeField] List<BuyProperty> buyProperties = new List<BuyProperty>();
    int tempInt;
    // Start is called before the first frame update
    private void Awake()//IEnumerator Start()
    {
        //yield return new WaitUntil(() => SceneManager.GetActiveScene().isLoaded);
        
        if (buyProperties.Count > 0)
        {
            for (int i = 0; i < buyProperties.Count; i++)
            {
                if (!buyProperties[i].IsActive && i != 0)
                        buyProperties[i].BuyColliderGo.SetActive(false);
                
                buyProperties[i].OnBuyProperty += ActivateAfterMe;
            }
        }

        //StopCoroutine(Start());
    }

    private void Armut()
    {
        if (buyProperties.Count > 0)
        {
            for (int i = 0; i < buyProperties.Count; i++)
            {
                if (!buyProperties[i].IsActive)
                {
                    if (i != 0)
                        buyProperties[i].BuyColliderGo.SetActive(false);
                }
                else if (buyProperties[i].IsActive)
                {

                    ActivateAfterMe(buyProperties[i]);
                }

                buyProperties[i].OnBuyProperty += ActivateAfterMe;
            }
        }

        
    }

    
    private void OnDestroy()
    {
        if (buyProperties.Count > 0)
        {
            for (int i = 0; i < buyProperties.Count; i++)
                buyProperties[i].OnBuyProperty -= ActivateAfterMe;
        }
    }
    
    private void ActivateAfterMe(BuyProperty buyProperty)
    {
        tempInt = buyProperties.IndexOf(buyProperty);

        if (tempInt != buyProperties.Count - 1)
            buyProperties[tempInt + 1].BuyColliderGo.SetActive(true);
    }
}
