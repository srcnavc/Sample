using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect 
{
    string _EffectTag { get; }
    void doEffect(GameObject target);
}


public enum EffectTarget
{
    self,
    other,
    bought
}
