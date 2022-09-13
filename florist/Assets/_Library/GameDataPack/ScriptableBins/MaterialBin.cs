using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialBin", menuName = "Scriptables/Data/MaterialBin")]
public class MaterialBin : ScriptableObject
{
    public List<Material> materials;
}
