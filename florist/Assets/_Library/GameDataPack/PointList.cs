using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointList", menuName = "Scriptables/General/PointList")]

public class PointList : ScriptableObject
{
    public List<Vector3> Positions = new List<Vector3>();
}
