﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ZoneIdentity))]
public class ZoneChangeTrigger : MonoBehaviour
{
    public UnityEvent OnTheActiveZone;
    public UnityEvent OffTheActiveZone;
    ZoneIdentity identity;
    string activeId;
    bool isCalledFirstTime;

    // Start is called before the first frame update
    void Awake()
    {
        identity = GetComponent<ZoneIdentity>();
        InfiniteRoad.OnActiveZoneChanged += OnActiveZoneChange;
    }
    private void LateUpdate()
    {
        if (!isCalledFirstTime)
        {
            if (activeId == identity.Id)
                OnTheActiveZone?.Invoke();
            else
                OffTheActiveZone?.Invoke();

            isCalledFirstTime = true;
            //Debug.Log("zone changer " + Time.time);
        }
    }

    private void OnDestroy()
    {
        InfiniteRoad.OnActiveZoneChanged -= OnActiveZoneChange;
    }

    private void OnActiveZoneChange(string id)
    {
        activeId = id;
        if (id == identity.Id)
            OnTheActiveZone?.Invoke();
        else
            OffTheActiveZone?.Invoke();
    }
}
