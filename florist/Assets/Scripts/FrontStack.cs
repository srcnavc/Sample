using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontStack : MonoBehaviour
{
    public static FrontStack ins;
    [SerializeField] GameObject player;
    [SerializeField] Transform container;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Vector3Int currentLocation;
    [SerializeField] Vector3 tileSize;
    [SerializeField] int currentStackSize;
    [SerializeField] PoolInfo blueStackPrefabInfo;
    [SerializeField] PoolInfo redStackPrefabInfo;
    [SerializeField] PoolInfo yellowStackPrefabInfo;
    [SerializeField] Vector3 holderOffset;
    [SerializeField] List<GameObject> items = new List<GameObject>();

    public int CurrentStackCount => items.Count;
    Vector3 tempVec3;
    GameObject tempGo;
    IStackItem tempStackItem;
    int itemIndex = 0;

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentLocation = Vector3Int.zero;
    }

    string tempPoolName;

    private string GetPoolName(FlowerTypeSC typeSC)
    {

        switch (typeSC.Color)
        {
            case FlowerColor.Blue:
                tempPoolName = blueStackPrefabInfo.PoolName;
                break;
            case FlowerColor.Red:
                tempPoolName = redStackPrefabInfo.PoolName;
                break;
            case FlowerColor.Yellow:
                tempPoolName = yellowStackPrefabInfo.PoolName;
                break;
            default:
                tempPoolName = "";
                break;
        }

        return tempPoolName;
    }
    FlowerTypeSC tempFlowerTypeSC;
    public void AddItem(GameObject flowerGo)
    {
        tempFlowerTypeSC = flowerGo.GetComponent<FlowerType>().Type;

        if (tempFlowerTypeSC == null)
            Debug.Log("Flower Type is null.");
        
        tempGo = PoolManager.fetch(GetPoolName(tempFlowerTypeSC));
        
        tempStackItem = tempGo.GetComponent<IStackItem>();
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);
        tempGo.name = "Axe - " + "(" + currentLocation.x + ", " + currentLocation.z + ")";
        
        tempStackItem.ZPositionOffset = tileSize.z;
        tempGo.transform.position = container.position; //+ Getlocation(currentLocation);
        
        if (items.Count >= stackSize.x)
            tempStackItem.BeforeMe = items[items.Count - stackSize.x];
        else
            tempStackItem.BeforeMe = container.gameObject;
        

        items.Add(tempGo);
        tempStackItem.IsActive = true;
        tempGo.SetActive(true);

        currentStackSize++;
        currentLocation.x++;

        if (currentLocation.x >= stackSize.x)
        {
            currentLocation.x = 0;
            currentLocation.z++;
            if (currentLocation.z >= stackSize.z)
            {
                currentLocation.z = 0;
                currentLocation.y++;
                if (currentLocation.y >= stackSize.y)
                {
                    currentLocation.y = 0;
                }
            }
        }
        //Debug.Log("(" + currentLocation.x + ", " + currentLocation.z + ")" + "  - " + tempGo.name);
    }
    
    public void RemoveItem()
    {
        Debug.Log("RemoveItem");
        if (items.Count >= 1)
            RemoveItem(items[items.Count - 1]);
    } /// RemoveItem(Go)

    public void Scatter(GameObject go)
    {
        //Debug.Log("Scatter");
        itemIndex = 0;
        tempStackItem = go.GetComponent<IStackItem>();
        if (items.Contains(go))
        {
            itemIndex = items.IndexOf(go);
            
            for (int i = items.Count - 1; i >= itemIndex; i--)
            {
                tempStackItem = items[i].GetComponent<IStackItem>();
                tempStackItem.Scatter();
                RemoveItem(items[i]);
            }
        }
        //Debug.Break();
    } /// RemoveItem(Go)

    public void ScatterAll()
    {
        //Debug.Log("ScatterAll");
        for (int i = items.Count - 1; i >= 0; i--)
        {
            tempStackItem = items[i].GetComponent<IStackItem>();
            tempStackItem.Scatter();
            RemoveItem(items[i]);
        }
    }  /// RemoveItem(Go)

    public void RemoveAll()
    {
        //Debug.Log("RemoveAll");
        
        for (int i = items.Count - 1; i >= 0 ; i--)
            RemoveItem(items[i]);
        
        items.Clear();
    }  /// RemoveItem(Go)

    public void RemoveItem(GameObject go)
    {
        itemIndex = 0;
        tempStackItem = go.GetComponent<IStackItem>();
        if (items.Contains(go))
        {
            itemIndex = items.IndexOf(go);
            currentLocation = tempStackItem.Location;
            currentStackSize = items.Count - itemIndex;

            items.Remove(go);
            go.GetComponent<PoolObject>().release();
            
            for (int i = itemIndex; i < items.Count; i++)
            {
                
                tempStackItem = items[i].GetComponent<IStackItem>();
                tempStackItem.Location = currentLocation;
                tempStackItem.AttachedGameObject.name = "Axe - " + "(" + currentLocation.x + ", " + currentLocation.z + ")";
                tempStackItem.ZPositionOffset = tileSize.z;
                tempStackItem.AttachedGameObject.transform.position = container.position + Getlocation(currentLocation);
                
                if (i >= stackSize.x)
                    tempStackItem.BeforeMe = items[i - stackSize.x];
                else
                    tempStackItem.BeforeMe = container.gameObject;
                
                currentStackSize++;
                currentLocation.x++;
                
                if (currentLocation.x >= stackSize.x)
                {
                    currentLocation.x = 0;
                    currentLocation.z++;
                    if (currentLocation.z >= stackSize.z)
                    {
                        currentLocation.z = 0;
                        currentLocation.y++;
                        if (currentLocation.y >= stackSize.y)
                        {
                            currentLocation.y = 0;
                        }
                    }
                }
            }
        }

        //Debug.Break();
    }
    private Vector3 Getlocation(Vector3Int pos)
    {
        tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);
        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        tempVec3 += holderOffset;
        return tempVec3;
    }
}

public interface IStackItem
{
    GameObject BeforeMe { get; set; }
    Vector3Int Location { get; set; }
    GameObject AttachedGameObject { get; }
    float ZPositionOffset { get; set; }
    float YPositionOffset { get; set; }
    bool IsActive { get; set; }
    Vector3 TargetLocalPosition { get; set; }
    void ResetParams();
    void Scatter();
    void StartMovingWithScaling(Vector3 startScale, Vector3 targetScale);
}
