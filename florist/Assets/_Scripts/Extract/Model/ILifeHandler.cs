using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ILifeHandler : IParameterHolder<float>
{
    event Action OnDamageTaken;
    event Action OnDeath;

    void getDamage(float amount);
   void die();
   void addLife(float amount);
   
}
