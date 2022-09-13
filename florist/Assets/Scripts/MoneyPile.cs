using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPile : MonoBehaviour
{
    [SerializeField] BackStackUp stackUp;
    string saveId => GetComponentInParent<LogPile>().SaveId + "_MoneyPile";
    int tempInt;

    private void Awake()
    {
        if(!PlayerPrefs.HasKey(saveId))
            PlayerPrefs.SetInt(saveId, 0);

        tempInt = PlayerPrefs.GetInt(saveId);

        for (int i = 0; i < tempInt; i++)
            stackUp.AddItem();
    }
    public void AddItemToPlayer()
    {
        if(stackUp.CurrentStackCount >= 0)
        {
            tempInt = stackUp.CurrentStackCount;
            for (int i = 0; i < tempInt; i++)
            {
                BackStackUp.ins.AddItemWithScaling(stackUp.GetLastStackItem().transform.position, Vector3.one, Vector3.one);
                stackUp.RemoveItem();
            }
        }
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt(saveId, stackUp.CurrentStackCount);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(saveId, stackUp.CurrentStackCount);
    }
}
