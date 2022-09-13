using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour, IPoolObject
{
    [SerializeField] string Tag;
    [SerializeField] int woodCount;
    [SerializeField] GameObject beforeMe;

    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    
    public GameObject BeforeMe { get => beforeMe; set => beforeMe = value; }
    public int WoodCount { get => Variables.GetInt("WoodCount"); }

    private void ResetParameters()
    {
        GetComponent<ILifeHandler>().addLife(GetComponent<ILifeHandler>().getMax());
        gameObject.name = "Log";
        transform.parent = null;
    }

    #region Pool
    public void clearForRelease()
    {
        ResetParameters();
    }

    public void OnCreate()
    {
        
    }

    public void resetForRotate()
    {
        ResetParameters();
    }

    #endregion
}
