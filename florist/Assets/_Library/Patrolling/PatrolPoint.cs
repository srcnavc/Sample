using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolPoint : MonoBehaviour,ISpecialBlock
{

    public PatrolType type;
    public PatrolPointType patrolPointType;
    public List<PatrolStation> StationPoints;
    List<PatrolStation> PointsInUse; 

    int currentStationIndex;
    int zigzagAditive = 1;
    private void Awake()
    {
        PointsInUse = new List<PatrolStation>();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < StationPoints.Count; i++)
        {

            Gizmos.color = Color.red;
            if(patrolPointType==PatrolPointType.optional)
            Gizmos.color = Color.green;

            Gizmos.DrawSphere(StationPoints[i].point,.4f);

            if (StationPoints.Count - 1 > i)
            {
                Gizmos.DrawLine(StationPoints[i].point, StationPoints[i + 1].point);
            }
            else if (StationPoints.Count - 1 == i && type == PatrolType.Rotate)
            {
                Gizmos.DrawLine(StationPoints[i].point, StationPoints[0].point);
            }


            Gizmos.color = Color.blue;
            
            Gizmos.DrawLine(StationPoints[i].point, StationPoints[i].point + Quaternion.Euler(0,StationPoints[i].WaitRotateAngle,0)* Vector3.forward*2);
        }
    }

    public PatrolStation getCurrentStation()
    {
        return StationPoints[currentStationIndex];

    }
    public int getCurrentStationIndex()
    {

        return currentStationIndex;
    }
    public int getNextStationIndex()
    {
        

        switch (type)
        {
            case PatrolType.ZigZag:
                if (currentStationIndex == StationPoints.Count - 1)
                    zigzagAditive = -1;
                if (currentStationIndex == 0)
                    zigzagAditive = 1;
                currentStationIndex += zigzagAditive;

                break;
            case PatrolType.Rotate:
                currentStationIndex = (currentStationIndex + 1) % StationPoints.Count;
                break;
            case PatrolType.Random:
                currentStationIndex = UnityEngine.Random.Range(0,StationPoints.Count);
                break;
            case PatrolType.OccupiedRandom:
                currentStationIndex = UnityEngine.Random.Range(0, StationPoints.Count);
                break;
            default:
                break;
        }


        return currentStationIndex;

    }

    public PatrolStation getNextStation()
    {
        int nextIndex = getNextStationIndex();
        PatrolStation selectedStation = StationPoints[nextIndex];
        
        selectedStation.isOccupied = true;
        if (type== PatrolType.OccupiedRandom)
        {
            StationPoints.Remove(selectedStation);
            PointsInUse.Add(selectedStation);
        }
        return selectedStation;
    }

    public void releaseOccupied(PatrolStation station)
    {

        if (station!=null && station.isOccupied)
        {
            station.isOccupied = false;
            PointsInUse.Remove(station);
            StationPoints.Add(station);
        }
    }

    public void getSpecialParameters(ref List<specialData> specials)
    {
      // throw new NotImplementedException();
    }

    public void setSpecialParameters(List<specialData> specials)
    {
      //  throw new NotImplementedException();
    }
}
[Serializable]
public class PatrolStation
{
    public Vector3 point;
    public float waitTime;
    public float WaitRotateAngle;
    public bool isOccupied;
}
public enum PatrolType { 
ZigZag = 0,
Rotate = 1,
Random = 2,
OccupiedRandom =3

}

public enum PatrolPointType
{ 
mandatory = 0 ,
optional = 1
}

public enum PatrolState { 
Patroling =  0,
PatrolPause = 1,
Stationed = 2,
Alarmed = 3,
Knocked = 4,
Dead = 5
}


