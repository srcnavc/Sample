using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerTrigger : MonoBehaviour,ISwitchTrigger
{
    public UnityEvent StartTrigger, EndTrigger;
    public string SwitchTag;
    public UnityEvent _TriggerOffEvent => EndTrigger;

    public UnityEvent _TriggerEvent => StartTrigger;
    private IEnumerator timerCoroutine;
    float sleepDuration = 0;
    bool running = false;

    public void startTimer(float intime)
    {

        sleepDuration += intime;

       
        if (!running)
        {
            timerCoroutine = Timer();
            StartTrigger.Invoke();
            StartCoroutine(timerCoroutine);
            running = true;
        }
        
    }
    

    IEnumerator Timer()
    {

        while (sleepDuration > 0)
        {
            yield return new WaitForSeconds(1);
            sleepDuration -= 1;
        }
        EndTrigger.Invoke();
        running = false;
    }
}
