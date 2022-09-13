using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterController : MonoBehaviour
{
    public static RegisterController ins;
    public static List<RegisterController> registers = new List<RegisterController>();
    RegisterController activeRegister;
    [SerializeField] ZoneIdentity identity;
    [SerializeField] RegisterPile leftPile;
    [SerializeField] RegisterPile rightPile;
    [SerializeField] CurrencySC relatedCUrrency;
    
    private void Awake()
    {
        if (ins == null)
            ins = this;

        AddRegister(this);
    }

    int index;
    public Transform GetActiveRegisterTransform()
    {
        for (int i = 0; i < registers.Count; i++)
        {
            if (registers[i].identity.Id == InfiniteRoad.ins.ActiveZoneId)
            {
                activeRegister = registers[i];
                return activeRegister.transform;
            }
        }

        index = registers.IndexOf(activeRegister) + 1;
        activeRegister = registers[index % registers.Count];

        return activeRegister.transform;
    }
    private void AddRegister(RegisterController register)
    {
        if (!registers.Contains(register))
            registers.Add(register);
    }
    public void AddWithScaling(Vector3 from, Vector3 startScale, Vector3 targetScale)
    {
        if ((leftPile.CurrentStackCount + rightPile.CurrentStackCount) % 2 == 0)
            leftPile.AddItemWithScaling(from,startScale,targetScale);
        else
            rightPile.AddItemWithScaling(from, startScale, targetScale);
    }

    int tempInt;
    public void AddItemToPlayer()
    {
        if ((leftPile.CurrentStackCount + rightPile.CurrentStackCount) <= 0)
            return;

        tempInt = leftPile.CurrentStackCount + rightPile.CurrentStackCount;
        for (int i = 0; i < tempInt; i++)
        {
            
            if ((leftPile.CurrentStackCount + rightPile.CurrentStackCount) % 2 == 0 && rightPile.CurrentStackCount > 0)
            {
                //BackStackUp.ins.AddItemWithScaling(rightPile.GetLastStackItem().transform.position, Vector3.zero, Vector3.one);
                
                rightPile.RemoveItem();
            }
            else if ((leftPile.CurrentStackCount + rightPile.CurrentStackCount) % 2 == 1 && leftPile.CurrentStackCount > 0)
            {
                //*BackStackUp.ins.AddItemWithScaling(leftPile.GetLastStackItem().transform.position, Vector3.zero, Vector3.one);

                leftPile.RemoveItem();
            }

            relatedCUrrency.Value++;
        }

        
    }
}
