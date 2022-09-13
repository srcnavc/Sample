using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public interface IParameterHolder<T>
{
    

    event Action<T> onValueUpdate;
    T getCurrent();
    T getMax();
    T getPercent();
    bool tagCheck(String tag);

}
