using System;

using UnityEngine;



[Serializable]
public abstract  class ScriptableParameter : ScriptableObject
{
    public event Action OnParameterUpdate;
    public bool isRecording;
    public abstract string toString();

    public void ParameterChanged()
    {
        OnParameterUpdate?.Invoke();
        
    }

}


