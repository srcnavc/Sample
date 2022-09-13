using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DurationActivated : Item
{

    float startTime;
    [SerializeField] float delay;

    public void SetDelay(float delay)
    {
        this.delay = delay;
    }
    void Update()
    {
        if (startTime + delay < Time.time)
        {
            startTime = Time.time;
            activate(gameObject);

        }
    }
}
