using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneIdentity : MonoBehaviour
{
    public string Id;
    [SerializeField] bool isIdSet = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isIdSet)
        {
            Id = transform.root.GetComponent<MapComponents>().ZoneId;
            isIdSet = true;
        }
    }
}
