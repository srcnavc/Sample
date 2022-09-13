using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Flower Spawn List", menuName = "Scriptables/Flower Spawn List")]
public class FlowersSpawnRatio : ScriptableObject
{
    public List<FlowerSpawnProbability> Ratios = new List<FlowerSpawnProbability>();
    
    public float GetRatio(FlowerTypeSC flowerType)
    {
        return Ratios.Find(x => x.type == flowerType).ratio;
    }

    public PoolInfo GetPoolInfo(FlowerTypeSC flowerType)
    {
        return Ratios.Find(x => x.type == flowerType).PoolInfo;
    }
    private void OnValidate()
    {
        for (int i = 0; i < Ratios.Count; i++)
        {
            if (Ratios[i].type != null)
                Ratios[i].name = Ratios[i].type.Name + " - " + Ratios[i].ratio;
        }
    }
}
[System.Serializable]
public class FlowerSpawnProbability
{
    [HideInInspector] public string name;
    public FlowerTypeSC type;
    public PoolInfo PoolInfo;
    [Range(0f, 1f)]
    public float ratio;
}
