using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

public class EffectHaptics : MonoBehaviour,IEffect
{
    public HapticTypes Haptic_Type;
    public string EffectTag;
    
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
        MMVibrationManager.Haptic(Haptic_Type);
 
    }

    
}
