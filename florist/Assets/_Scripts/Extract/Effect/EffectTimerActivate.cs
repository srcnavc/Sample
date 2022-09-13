using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimerActivate : MonoBehaviour,IEffect
{
    public string _EffectTag => throw new System.NotImplementedException();
    public float duration;
    public string timerTag;
    public void doEffect(GameObject target)
    {
        TimerTrigger tt = target.GetComponent<TimerTrigger>();
        if (tt != null&&tt.SwitchTag== timerTag)
        {
            tt.startTimer(duration);
        }
        
    }

   
}
