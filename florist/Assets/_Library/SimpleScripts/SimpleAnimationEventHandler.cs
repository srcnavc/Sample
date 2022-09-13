using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimationEventHandler : MonoBehaviour
{
    public delegate void completed();
    List<completed> eventListeners;

    private void Awake()
    {
        eventListeners = new List<completed>();

    }

    public void addListener(completed listener)
    {

        eventListeners.Add(listener);

    }

    public void AnimationEvent()
    {
        for (int i = 0; i < eventListeners.Count; i++)
        {

            eventListeners[i].Invoke();
        }
    }

}
