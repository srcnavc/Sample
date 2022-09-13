using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITarget 
{
    Vector3 getObjectPosition();
    Vector3 getTargetPosition();

    GameObject GetGameObject();

    bool isValid();


}
