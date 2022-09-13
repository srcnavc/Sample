using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] List<Land> lands = new List<Land>();
    [SerializeField] Land lastActivatedLand = null;
    [SerializeField] List<CollectableFlower> collectableFlowers = new List<CollectableFlower>();
    [SerializeField] FlowerTypeSC red;
    [SerializeField] FlowerTypeSC yellow;
    [SerializeField] FlowerTypeSC blue;

    int redFlowerCount;
    int yellowFlowerCount;
    int blueFlowerCount;
    int randomInt;
    int index;

    [System.Serializable]
    public class Land
    {
        public string name;
        public string landId;
        public FlowersSpawnRatio spawnRatio;
    }



    // Start is called before the first frame update
    void Start()
    {
        collectableFlowers.AddRange(GetComponentsInChildren<CollectableFlower>());

        for (int i = 0; i < lands.Count; i++)
        {
            if (PlayerPrefs.GetInt(lands[i].landId + "_Property") == 1)
                lastActivatedLand = lands[i];
        }

        if(lastActivatedLand.spawnRatio == null)
            return;

        redFlowerCount = (int)(collectableFlowers.Count * lastActivatedLand.spawnRatio.GetRatio(red));
        blueFlowerCount = (int)(collectableFlowers.Count * lastActivatedLand.spawnRatio.GetRatio(blue));
        yellowFlowerCount = (int)(collectableFlowers.Count * lastActivatedLand.spawnRatio.GetRatio(yellow));

        index = 0;
        int failsafe = 0;
        while (failsafe <= 100 && index < collectableFlowers.Count && (redFlowerCount > 0 || blueFlowerCount > 0 || yellowFlowerCount > 0))
        {
            failsafe++;
            randomInt = UnityEngine.Random.Range(0, 3);

            switch (randomInt)
            {
                case 0:
                    if (redFlowerCount > 0)
                    {
                        SwitchFlower(collectableFlowers[index], red);
                        redFlowerCount--;
                        index++;
                    }
                    break;
                case 1:
                    if (yellowFlowerCount > 0)
                    {
                        SwitchFlower(collectableFlowers[index], yellow);
                        yellowFlowerCount--;
                        index++;
                    }
                    break;
                case 2:
                    if (blueFlowerCount > 0)
                    {
                        SwitchFlower(collectableFlowers[index], blue);
                        blueFlowerCount--;
                        index++;
                    }
                    break;
                default:
                    break;
            }
        }


    }

    int failSafe;
    private void SwitchFlower(CollectableFlower flower, FlowerTypeSC type)
    {
        GameObject newCollectable = PoolManager.fetch(lastActivatedLand.spawnRatio.GetPoolInfo(type).PoolName);
        failSafe = 0;

        while (newCollectable == null && failSafe <= 100)
        {
            failSafe++;
            newCollectable = PoolManager.fetch(lastActivatedLand.spawnRatio.GetPoolInfo(type).PoolName);
        }

        if (newCollectable == null)
            Debug.Log("still null");

        newCollectable.transform.parent = transform;
        newCollectable.transform.position = flower.transform.position;
        newCollectable.GetComponent<CollectableFlower>().IsAvailable = true;
        newCollectable.SetActive(true);
        
        flower.gameObject.SetActive(false);
    }
}
