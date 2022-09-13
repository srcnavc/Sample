using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class ScriptableAlertingParameter<T> : ScriptableParameter
{
    T _PValue;
    [SerializeField]protected T DefaultValue;
    public virtual T PValue
    {
        get
        {

            return _PValue;
        }

        set
        {
            if (_PValue==null||!_PValue.Equals(value))
            {
                _PValue = value;
                
                ParameterChanged();
                if(isRecording)
                    SavePrefs();
            }
        }
    }

    protected abstract void SavePrefs();
    protected abstract T readPrefs();

    private void OnEnable()
    {
        _PValue = DefaultValue;
  
        if(isRecording)
            _PValue = readPrefs();
    }

}


