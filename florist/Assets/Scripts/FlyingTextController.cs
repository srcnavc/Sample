using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingTextController : MonoBehaviour
{
    public bool isEnabled = false;
    [SerializeField] PoolInfo flyingTextInfo;
    [SerializeField] Transform spawnLocation;
    [SerializeField] string text;
    
    GameObject tempGo;
    FlyingText tempFlyingText;

    private void Start()
    {
        spawnLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Enabled()
    {
        isEnabled = true;
    }

    public void Disabled()
    {
        isEnabled = false;
    }
    public void SpawnText()
    {
        if(spawnLocation != null && isEnabled)
        {
            tempGo = PoolManager.fetch(flyingTextInfo.PoolName);
            tempGo.transform.parent = spawnLocation;
            tempGo.transform.localPosition = Vector3.zero;
            tempFlyingText = tempGo.GetComponent<FlyingText>();
            tempGo.SetActive(true);
            tempFlyingText.StartText(spawnLocation.position, text);
        }
        else if(spawnLocation == null)
        {
            Debug.Log("Spawn Location is null.");
        }
        
    }
}
