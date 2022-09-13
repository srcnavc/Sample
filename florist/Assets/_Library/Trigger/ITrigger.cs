using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public interface ITrigger<T>
{
     UnityEvent<T> _TriggerEvent { get; }
   
}

public interface ITrigger
{
    UnityEvent _TriggerEvent { get; }

}

