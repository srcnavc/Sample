using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRunwayNavmesh : MonoBehaviour
{
    [SerializeField] GameObject runway;
    [SerializeField] int activeLayerId;
    [SerializeField] int deactiveLayerId;
    public void Disable()
    {
        runway.layer = deactiveLayerId;
        InfiniteRoad.ins.BuildNavmesh();
    }

    public void Enable()
    {
        runway.layer = activeLayerId;
        InfiniteRoad.ins.BuildNavmesh();
    }
}
