using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


[CreateAssetMenu(fileName ="Level" , menuName ="Scriptables/Level/Data")]
public class LevelData : ScriptableObject
{
    public blockData[] blocks;


    public GameObject CreateStaticObject(Transform parent, Vector3 position)
    {
        GameObject block = createObject(parent, position);
        Rigidbody rb = block.GetComponent<Rigidbody>();
        if(rb == null)
            rb = block.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
        block.isStatic = true;
        return block;

    }
    public GameObject createObject(Transform parent, Vector3 position)
    {


        GameObject block = new GameObject(name + "-Z" + position.z);
        block.transform.parent = parent;
        block.transform.localPosition = position;
        foreach (blockData bd in blocks)
        {
            GameObject temp = Instantiate(bd.prefab);
            temp.transform.parent = block.transform;
            temp.transform.localScale = bd.scale;
            temp.transform.localPosition = bd.position;
            temp.transform.localRotation = bd.rotation;
            temp.SetActive(bd.enabled);

        }
        return block;
    }
#if UNITY_EDITOR

    public GameObject RecoverObject(Transform parent, Vector3 position)
    {
        GameObject block = new GameObject(name + "-Z" + position.z);
        block.transform.parent = parent;
        block.transform.localPosition = position;
        foreach (blockData bd in blocks)
        {
            GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(bd.prefab);
            temp.transform.parent = block.transform;
            temp.transform.localScale = bd.scale;
            temp.transform.localPosition = bd.position;
            temp.transform.localRotation = bd.rotation;
            temp.SetActive(bd.enabled);
            ISpecialBlock[] Isps = temp.GetComponents<ISpecialBlock>();
            foreach(ISpecialBlock Isp in Isps)
            {
                Isp.setSpecialParameters(bd.specials);
            }
        }

        return block;
    }
#endif

    public GameObject RecoverObjectLive(Transform parent, Vector3 position)
    {
        GameObject block = new GameObject(name + "-Z" + position.z);
        block.transform.parent = parent;
        block.transform.localPosition = position;
        foreach (blockData bd in blocks)
        {
            GameObject temp = Instantiate(bd.prefab);
            temp.transform.parent = block.transform;
            temp.transform.localScale = bd.scale;
            temp.transform.localPosition = bd.position;
            temp.transform.localRotation = bd.rotation;
            temp.SetActive(bd.enabled);
            ISpecialBlock[] Isps = temp.GetComponents<ISpecialBlock>();
            foreach (ISpecialBlock Isp in Isps)
            {
                Isp.setSpecialParameters(bd.specials);
            }
        }

        return block;
    }

}

[Serializable]
public class blockData
{
    public GameObject prefab;
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
    public bool enabled;
    public List<specialData> specials;

}
[Serializable]
public class specialData
{
    public specialData(string SpecialKey, string Data)
    {
        this.SpecialKey = SpecialKey;
        this.Data = Data;
    }
    public string SpecialKey;
    public string Data;

}




