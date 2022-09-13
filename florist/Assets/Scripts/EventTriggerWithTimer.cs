using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerWithTimer : MonoBehaviour
{
    public UnityEvent Event;
    [SerializeField] float delay;
    [SerializeField] bool isTimerStarted;
    [SerializeField] float timer;
    public void StartTimer()
    {
        timer = Time.time + delay;
        isTimerStarted = true;
       // FillImage.ins.Activate();
    }

    public void StopTimer()
    {
        //FillImage.ins.Deactivate();
        isTimerStarted = false;
        timer = 0f;
    }

    public float RemainingPercentage
    {
        get => 1 - ((timer - Time.time) / delay);
    }

    // Update is called once per frame
    void Update()
    {
       // if(isTimerStarted)
         //   FillImage.ins.Fill(RemainingPercentage);

        if (timer <= Time.time && isTimerStarted)
        {
            Event?.Invoke();
            StopTimer();
        }
    }
}
