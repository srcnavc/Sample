using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyHolder : MonoBehaviour,IEffect
{
    [SerializeField] string EffectTag = "default";
    public string _EffectTag => EffectTag;
    public void doEffect(GameObject target)
    {
        Destroy(gameObject);
    }


}
