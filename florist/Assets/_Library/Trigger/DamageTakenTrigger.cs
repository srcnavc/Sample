using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DamageTakenTrigger : MonoBehaviour,ITrigger
{
    public UnityEvent DamageTaken;
    public UnityEvent _TriggerEvent => DamageTaken;
    ILifeHandler lifeHandler;
    // Start is called before the first frame update
    void Start()
    {
        lifeHandler = GetComponent<ILifeHandler>();
        lifeHandler.OnDamageTaken += executeEvents;
    }

    void executeEvents()
    {
        DamageTaken.Invoke();
    }

    
}
