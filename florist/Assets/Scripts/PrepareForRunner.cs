using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareForRunner : MonoBehaviour
{
    [SerializeField] LogPile storage;
   
    public void PutAllPotsToStorage()
    {
        if (FrontStackUp.ins.CurrentStackCount < 1)
            return;

        storage.AddItemToPile = true;
    }
}
