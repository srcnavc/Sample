using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastCombiner : MonoBehaviour, IPhysicsCaster
{
    public sortModel Sorting;
    public int totalTargetCount;
    List<IPhysicsCaster> Casters;
    [SerializeField]List<ITarget> targets;
    [SerializeField]List<GameObject> Vievtargets;

    public bool active;
    TargesDistanceComperator sortByDistance;
    public List<IPhysicsCaster> getCasters()
    {
        if (Casters == null)
        {
            Casters = new List<IPhysicsCaster>();
            Casters.AddRange(GetComponents<IPhysicsCaster>());
            Casters.Remove(this);

        }
        return Casters;
    
    }

    public void cast(Vector3 Origin, Vector3 Direction)
    {
        if (sortByDistance == null)
            sortByDistance = new TargesDistanceComperator(Origin);
        if (targets == null)
            targets = new List<ITarget>();

        if(targets.Count>0)
        targets.RemoveRange(0,targets.Count);
        for (int i = 0; i < getCasters().Count; i++)
        {
            getCasters()[i].cast(Origin, Direction);
            if (getCasters()[i].getTargets().Count>0)
                targets.AddRange(getCasters()[i].getTargets());
        }

        if (Sorting == sortModel.closest)
        {
            sortByDistance.setComperator(Origin);
            targets.Sort(sortByDistance);
        }

        totalTargetCount = targets.Count;
       // createViews();
    }

    public void createViews()
    {
        Vievtargets = new List<GameObject>();
        for (int i = 0; i < targets.Count; i++)
            if(targets[i]!=null)
            Vievtargets.Add(targets[i].GetGameObject());
    }

    public List<ITarget> getTargets()
    {
        return targets;
    }

    public bool isActive()
    {
        return active;
    }


}

public enum sortModel
{
    closest,
    noSorting

}

public class TargesDistanceComperator : IComparer<ITarget>
{

    public Vector3 Pos;

    public TargesDistanceComperator(Vector3 Pos)
    {
        this.Pos = Pos;

    }

    public void setComperator(Vector3 pos)
    {
        this.Pos = pos;
    }
    public int Compare(ITarget x, ITarget y)
    {
        x.GetGameObject().name = "Dist:" + Vector3.Distance(Pos, x.getObjectPosition());
        y.GetGameObject().name = "Dist:" + Vector3.Distance(Pos, y.getObjectPosition());

        if (x == null)
            return 1;
        if (y== null)
            return -1;

        if (Vector3.Distance(Pos, x.getObjectPosition()) > Vector3.Distance(Pos, y.getObjectPosition()))
             return 1;
        else
            return -1;

    }
}   
