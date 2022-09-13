using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatrolPoint),true)]
public class PatrolPointEditor : Editor
{
    private void OnSceneGUI()
    {

        PatrolPoint patrol = target as PatrolPoint;
        EditorUtility.SetDirty(patrol);
        if (patrol.StationPoints == null)
            patrol.StationPoints = new List<PatrolStation>();

        for (int i = 0; i < patrol.StationPoints.Count; i++)
        {
            Handles.Label(patrol.StationPoints[i].point + Vector3.up * 2, "Point" +i);
            patrol.StationPoints[i].point = Handles.PositionHandle(patrol.StationPoints[i].point, Quaternion.identity);
        }

      
        
    }
}
