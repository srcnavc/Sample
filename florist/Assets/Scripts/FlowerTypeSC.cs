using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Flower Type", menuName = "Scriptables/Flower Type")]
public class FlowerTypeSC : ScriptableObject
{
    public string Name;
    public float BloomTime;
    public int SellPrice;
    public FlowerColor Color;
}
