using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRootName : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("root : " + transform.root.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
            Debug.Log("root : " + transform.root.name);
    }
}
