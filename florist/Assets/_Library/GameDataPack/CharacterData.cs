using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptables/Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public GameObject model;
    public GameObject RagdollModel;
    public MaterialBin modelMaterials;
}
