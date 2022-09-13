using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public string PoolName;
    ILifeHandler _consumedParameter;
    ILifeHandler consumedParameter {
        get {
            if (_consumedParameter == null)
                _consumedParameter = GetComponentInParent<ILifeHandler>();

            return _consumedParameter;
        }
    }
    public float lastSpawn;
    public float delay;
    public float consumePerSpawn;
    TargetSelector4Spline _detector;
    TargetSelector4Spline detector
    {
        get { 
        if(_detector==null)
                _detector = GetComponentInParent<TargetSelector4Spline>();

            return _detector;
        }
    }
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (lastSpawn+delay<Time.time&& detector.getCurrentTarget()!=null)
        {
            GameObject currentProjectile = PoolManager.fetch(PoolName);
            currentProjectile.GetComponent<Projectile>()?.fire(transform.position, detector.getCurrentTarget().getTargetPosition());
            consumedParameter.getDamage(consumePerSpawn);
            lastSpawn = Time.time;
        }
    }
}
