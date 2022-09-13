using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerType : MonoBehaviour
{
    [SerializeField] FlowerTypeSC type;

    public FlowerTypeSC Type { get => type; set => type = value; }
}

public enum FlowerColor
{
    Blue,
    Red,
    Yellow
}
