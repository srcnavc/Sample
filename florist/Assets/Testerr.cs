using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Testerr : MonoBehaviour
{
    public ListWithCallback<GameObject> games = new ListWithCallback<GameObject>();
    public GameObject pref;
    public UnityEvent asd;
    public NavMeshSurface surface;
    private void Start()
    {
        //games.OnListCountChanged += OnChanged;

        

        
        
    }

    public void Create()
    {
        surface.BuildNavMesh();
    }

    private void OnChanged()
    {
        Debug.Log("0");
    }

    private void OnChanged1()
    {
        Debug.Log("1");
    }

    private void OnChanged2()
    {
        Debug.Log("2");
    }

    private void OnChanged3()
    {
        Debug.Log("3");
    }
}
