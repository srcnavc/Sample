using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPoolRelease : MonoBehaviour,IEffect
{

    public string EffectTag = "default";
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
        GetComponent<PoolObject>()?.release();
    }

}
