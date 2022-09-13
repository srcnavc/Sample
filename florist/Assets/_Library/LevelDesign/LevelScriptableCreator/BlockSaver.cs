using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BlockSaver : MonoBehaviour
{
    public string BlockName;
    public string BlockDir;


#if UNITY_EDITOR
    public void saveBlock()
    {
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        AssetDatabase.CreateAsset(asset, "Assets/"+BlockDir+"/"+BlockName+".asset");
        asset.blocks = new blockData[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            asset.blocks[i] = new blockData();
            asset.blocks[i].prefab = PrefabUtility.GetCorrespondingObjectFromSource(transform.GetChild(i).gameObject);
            //asset.blocks[i].prefab = (GameObject)EditorUtility.GetPrefabParent(transform.GetChild(i).gameObject) ;
            asset.blocks[i].position = transform.GetChild(i).localPosition;
            asset.blocks[i].scale = transform.GetChild(i).localScale;
            asset.blocks[i].rotation = transform.GetChild(i).localRotation;
            asset.blocks[i].enabled = transform.GetChild(i).gameObject.activeSelf;
            asset.blocks[i].specials = new List<specialData>();
            ISpecialBlock[] iSpecial = transform.GetChild(i).gameObject.GetComponents<ISpecialBlock>();
            for (int j = 0; j < iSpecial.Length; j++)
            {

                 iSpecial[j].getSpecialParameters(ref asset.blocks[i].specials);
            }
        }
        
        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();
    }

    public void saveAsPrefab()
    { 
    PrefabUtility.SaveAsPrefabAsset(gameObject , "Assets" + BlockDir + "" + BlockName + ".prefab");

    }
#endif

}
