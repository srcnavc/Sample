using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDeathTrigger : MonoBehaviour
{
    public UnityEvent OnDeath;
    public UnityEvent _TriggerEvent => OnDeath;
    ILifeHandler lifeHandler;
    void Start()
    {
        lifeHandler = GetComponent<ILifeHandler>();
        lifeHandler.OnDeath += executeEvents;
    }
    void executeEvents()
    {
        OnDeath.Invoke();
    }

}
