using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockSaver))]
public class BlockSaverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Save Asset"))
        {
            (target as BlockSaver).saveBlock();
        }

        if (GUILayout.Button("Save Prefab"))
        {
            (target as BlockSaver).saveAsPrefab();
        }

        base.OnInspectorGUI();
    }
}
