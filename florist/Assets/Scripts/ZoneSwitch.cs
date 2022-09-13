using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSwitch : MonoBehaviour
{
    [SerializeField] MapComponents mapComponents;

    InfiniteRoad road => InfiniteRoad.ins;
    public void EnterZone()
    {
        road.NextZone();
    }

    public void ExitZone()
    {

    }
}
