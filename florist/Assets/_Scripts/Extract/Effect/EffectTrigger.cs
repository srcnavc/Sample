using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    
    IEffect[] _effects;
    IEffect[] effects
    {
        get
        {
            if(_effects==null)
                _effects = GetComponents<IEffect>();

            return _effects;
        }

    }
    public void trigger(GameObject other , string effectTag)
    {
        for (int i = 0; i < effects.Length; i++)
            if(effectTag == "all" || effectTag == effects[i]._EffectTag)
            effects[i].doEffect(other);
    }

}
