using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public Pool pool;
    

    
    
    IPoolObject poolClient
    {
        get {
            if (_poolClient == null)
                _poolClient = GetComponent<IPoolObject>();
            return _poolClient;
        }

    }
    IPoolObject _poolClient;
    public void setPool(Pool pool)
    {
        this.pool = pool;
    }

    public void release()
    {

        if (poolClient != null)
            poolClient.clearForRelease();
        if(pool!=null)
        pool.release(this);

    }

    public void Reset()
    {
        if (poolClient != null)
            poolClient.resetForRotate();
    }

    public void connectToPool(string poolName)
    { 
    pool = PoolManager.getPoolByName(poolName);

    }

}
