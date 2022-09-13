using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAddToStack : MonoBehaviour,IStackable,IEffect
{
    public string EffectTag;
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
       IStackHolder stHolder =  target.GetComponent <IStackHolder> ();
        if (stHolder != null)
            stHolder.AddToStack(this);

    }

    public GameObject getGameObject()
    {
        return gameObject;
    }
}
