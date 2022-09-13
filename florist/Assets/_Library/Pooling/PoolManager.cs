using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    static PoolManager instance;
    public PoolCreateList poolCreateList;
    public List<PoolInfo> CreationList;
    public List<Pool> pools;
    public Hashtable PoolByName;
    public bool createOnStart;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    public static GameObject fetch(string itemName, bool isActive = false)
    {
        Pool pool = (Pool)instance.PoolByName[itemName];
        return pool.fetch(isActive);
    }

    public static bool IsCreated
    {
        get{
            return instance != null;
        }
    }
    public static void CreatePools()
    {
        if (instance.poolCreateList != null)
            instance.CreationList = instance.poolCreateList.CreationList;

        if(instance.pools==null)
            instance.pools = new List<Pool>();
        if (instance.PoolByName == null)
            instance.PoolByName = new Hashtable();
        for (int i = 0; i < instance.CreationList.Count; i++)
        {
            CreatePoolInternal(instance.CreationList[i]);
        }
    }

    private static void CreatePoolInternal(PoolInfo poolInfo)
    {
        Pool TempPool = new Pool();
        TempPool.SetPoolInfo(poolInfo);
        if (instance.PoolByName.ContainsKey(poolInfo.PoolName))
        {
            ((Pool)instance.PoolByName[poolInfo.PoolName]).releaseAll();
            instance.PoolByName[poolInfo.PoolName] = TempPool;
        }
        else
        {
            instance.PoolByName.Add(poolInfo.PoolName, TempPool);
            instance.pools.Add(TempPool);
        }
        TempPool.createObjects();
    }
    public static void CreatePool(PoolInfo poolInfo)
    {
        if (instance.pools == null)
            instance.pools = new List<Pool>();
        if (instance.PoolByName == null)
            instance.PoolByName = new Hashtable();

        Pool TempPool = new Pool();
        TempPool.SetPoolInfo(poolInfo);
        if (instance.PoolByName.ContainsKey(poolInfo.PoolName))
        {
            ((Pool)instance.PoolByName[poolInfo.PoolName]).releaseAll();
            instance.PoolByName[poolInfo.PoolName] = TempPool;
        }
        else
        {
            instance.PoolByName.Add(poolInfo.PoolName, TempPool);
            instance.pools.Add(TempPool);
        }
        TempPool.createObjects();
    }
    private void Start()
    {
        if (createOnStart)
        {
            CreatePools();
        }
    }
    
    public static void releaseAll()
    {
        foreach (Pool pool in instance.pools)
        {
            pool.releaseAll();
        }
    }

    public static Pool getPoolByName(string name)
    {
        return (Pool)instance.PoolByName[name];
        
    }

}
