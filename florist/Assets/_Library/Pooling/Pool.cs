using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool 
{
    static Vector3 nowhere = new Vector3(1000, 1000, 1000);
    PoolManager poolManager;
    public PoolInfo poolInfo;
    public List<GameObject> Pooled;
    public List<GameObject> InUse;
    public GameObject SamplePrefab {
        get {
            if (poolInfo.Prefab != null)
                return poolInfo.Prefab;
            else
                return InUse[0];
        }
    }

    public void SetPoolInfo(PoolInfo info)
    {

        poolInfo = info;
    }
    public GameObject extend()
    {
        GameObject tempObject = null;

        switch (poolInfo.ExtendModel)
        {

            case PoolInfo.ExtendType.Never:
                break;
            case PoolInfo.ExtendType.ForceCreate:
                tempObject = Object.Instantiate(SamplePrefab, nowhere, Quaternion.identity);
                PoolObject poolObject = tempObject.GetComponent<PoolObject>();
                if (poolObject == null)
                {
                    poolObject = tempObject.AddComponent<PoolObject>();
                   
                }
                poolObject.setPool(this);
                poolObject.Reset();
                InUse.Add(tempObject);
                break;
            case PoolInfo.ExtendType.ForceRotate:
                tempObject = InUse[0];
                tempObject.GetComponent<PoolObject>().Reset();
                InUse.Remove(tempObject);
                InUse.Add(tempObject);
                break;

        }


        
        return tempObject;
    
    }
    public GameObject fetch(bool isActive = false)
    {
        GameObject toreturn; 
        if (Pooled.Count > 0)
        {
            toreturn = Pooled[0];
            Pooled.Remove(toreturn);
            InUse.Add(toreturn);
            
        }
        else
            toreturn =  extend();

        if (toreturn != null && isActive)
            toreturn.SetActive(true);
       
        return toreturn;
        
    
    }

    public void createObjects()
    {
        Pooled = new List<GameObject>();
        InUse = new List<GameObject>();

        for (int i = 0; i < poolInfo.initSize; i++)
        {
            Pooled.Add(Object.Instantiate(poolInfo.Prefab ));

            PoolObject poolObject = Pooled[i].GetComponent<PoolObject>();
            if(poolObject==null)
                poolObject = Pooled[i].AddComponent<PoolObject>();
            Pooled[i].GetComponent<IPoolObject>().OnCreate();
            poolObject.gameObject.SetActive(false);
            poolObject.gameObject.transform.position = nowhere;
            poolObject.setPool(this);
        }
    
    }
    public void release(PoolObject poolObject)
    {
        release(poolObject.gameObject);
    }

    public void release(GameObject poolObject)
    {
        if (poolObject == null)
            return;
        poolObject.SetActive(false);
        InUse.Remove(poolObject);
        Pooled.Add(poolObject);

    }
    public void releaseAll()
    {
        while (InUse.Count > 0)
        {
            release(InUse[0]);
        }
    }





}
