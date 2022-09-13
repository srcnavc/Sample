using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;


public class HapticsManager : MonoBehaviour
{

    public void soft()
    {
        MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }


    public void hard()
    {
        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }


    
}
