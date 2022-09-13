using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITargetSelector 
{
    ITarget getTarget();
    List<ITarget> getTargets();
    event Action<ITarget> targetChanged;
}
