using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableData", menuName = "Scriptables/Data/CollectableData")]
public class CollectableData : CharacterData
{
    public delegate void endEffectCallBack();

    public ScriptableEffectBase effect;
    
    

    public GameObject VFX;
    public void doCollect(GameObject master , endEffectCallBack  callback)
    {
        effect.doEffect(master,callback.Invoke);
    }
    
}
