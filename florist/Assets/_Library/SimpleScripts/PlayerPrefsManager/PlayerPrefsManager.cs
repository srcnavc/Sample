using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public PrefKey[] PreferencesKeys;



}
[System.Serializable]
public class PrefKey
{
    public string name;
    public KeyType keytype;

    
}
public enum KeyType
{ 
INT = 0 ,
FLOAT = 1, 
STRING = 2
}
