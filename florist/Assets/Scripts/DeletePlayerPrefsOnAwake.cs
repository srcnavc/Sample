using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefsOnAwake : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
}
