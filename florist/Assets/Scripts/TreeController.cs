using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] int logCount;
    [SerializeField] Color color;
    [SerializeField] PoolInfo logPrefabInfo;
    [SerializeField] PoolInfo logBottomPrefabInfo;
    [SerializeField] PoolInfo logTopPrefabInfo;
    [SerializeField] Collider coll;

    float height = 1f;
    [SerializeField] float distance = 2f;
    [SerializeField] List<GameObject> logs = new List<GameObject>();
    GameObject tempGo;
    ILifeHandler tempLifeHandler;
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < logCount; i++)
        {
            tempGo = null;
            if (i == 0)
                tempGo = PoolManager.fetch(logBottomPrefabInfo.PoolName);
            else if(i == logCount - 1)
                tempGo = PoolManager.fetch(logTopPrefabInfo.PoolName);
            else
                tempGo = PoolManager.fetch(logPrefabInfo.PoolName);
            if (tempGo == null)
                Debug.Log("tempg null");

            tempGo.name = "Log " + i;

            tempGo.transform.parent = transform;
           // tempGo.GetComponent<Renderer>().material.color = color;
            tempGo.transform.localPosition = new Vector3(0f, height, 0f);
            height += distance;
            logs.Add(tempGo);

            if (i == 0)
                tempGo.GetComponent<LogController>().BeforeMe = null;
            else
                tempGo.GetComponent<LogController>().BeforeMe = logs[i - 1];

            tempGo.SetActive(true);
        }
    }
    
    public bool Damage(float dmg)
    {
        for (int i = 0; i < logs.Count; i++)
        {
            if (logs[i].activeSelf)
            {
                tempLifeHandler = logs[i].GetComponent<ILifeHandler>();
                tempGo = logs[i];

                if(dmg >= tempLifeHandler.getCurrent())
                    logs.Remove(logs[i]);
                
                tempLifeHandler.getDamage(dmg);

                if (logs.Count <= 0)
                    coll.enabled = false;
                
                return true;
            }
        }

        coll.enabled = false;
        return false;
    }
}
