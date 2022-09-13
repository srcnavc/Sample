using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ISwitchTrigger : ITrigger
{
    UnityEvent _TriggerOffEvent { get; }

}

public interface ISwitchTrigger<T> : ITrigger<T>
{
    UnityEvent<T> _TriggerOffEvent { get; }

}

