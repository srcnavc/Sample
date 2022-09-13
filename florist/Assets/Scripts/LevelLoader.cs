using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> levels = new List<GameObject>();
    [SerializeField] int currentLevel;

    public void Load(Transform LevelParent)
    {
        Delete(LevelParent);
        if(levels.Count > 0)
        {
            Instantiate(levels[currentLevel % levels.Count], LevelParent);
            currentLevel++;
        }
    }

    public void Delete(Transform LevelParent)
    {
        if(LevelParent.childCount > 0)
            Destroy(LevelParent.GetChild(0).gameObject);
    }
}
