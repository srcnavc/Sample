using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontStackUp : MonoBehaviour
{
    public static FrontStackUp ins;
    public Action<int> OnStackCountChanged;
    [SerializeField] Transform container;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Vector3Int currentLocation;
    [SerializeField] Vector3 tileSize;
    [SerializeField] int currentStackSize;
    [SerializeField] PoolInfo blueStackPrefabInfo;
    [SerializeField] PoolInfo redStackPrefabInfo;
    [SerializeField] PoolInfo yellowStackPrefabInfo;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] bool dontUseCurrency;
    [SerializeField] Vector3 holderOffset;
    public List<GameObject> items = new List<GameObject>();

    public int CurrentStackCount => items.Count;
    Vector3 tempVec3;
    GameObject tempGo;
    IStackItem tempStackItem;
    int itemIndex = 0;
    FlowerTypeSC tempFlowerTypeSC;

    private void Awake()
    {
        if (ins == null && gameObject.CompareTag("Player"))
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!dontUseCurrency)
            relatedCurrency.OnValueChanged += OnCurrencyChangedValue;
        currentLocation = Vector3Int.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            ins.AddItemWithScaling(blueStackPrefabInfo.Prefab, transform.position, Vector3.one, Vector3.one);
    }
    private void OnCurrencyChangedValue(int count)
    {
        /*if (items.Count > count)
        {
            tempInt = items.Count - count;
            for (int i = tempInt - 1; i >= 0; i--)
                RemoveItem(true);
        }
        else if (items.Count < count)
        {
            tempInt = count - items.Count;
            for (int i = 0; i < tempInt; i++)
                AddItem(true);
        }*/
    }

    public GameObject GetLastStackItem()
    {
        if (items.Count > 0)
            return items[items.Count - 1];
        else
            return null;
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

    public void AddItemWithScaling(GameObject flowerGo, Vector3 from, Vector3 startScale, Vector3 targetScale)
    {
        if(!dontUseCurrency)
            relatedCurrency.Value++;

        tempFlowerTypeSC = flowerGo.GetComponent<FlowerType>().Type;
        
        if (tempFlowerTypeSC == null)
            Debug.Log("Flower Type is null.");
        

        tempGo = PoolManager.fetch(GetPoolName(tempFlowerTypeSC));
        tempGo.transform.parent = container;
        tempGo.transform.localPosition = container.InverseTransformPoint(from);

        tempStackItem = tempGo.GetComponent<IStackItem>();
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);
        //tempStackItem.TargetLocalPosition = Getlocation(currentLocation);
        tempGo.name = "Flower Pot - " + "(" + currentLocation.y + ", " + currentLocation.x + ")";

        tempStackItem.YPositionOffset = tileSize.y;
        tempGo.transform.position = from; //+ Getlocation(currentLocation);

        if (items.Count >= stackSize.x)
            tempStackItem.BeforeMe = items[items.Count - stackSize.x];
        else
            tempStackItem.BeforeMe = container.gameObject;

        tempStackItem.TargetLocalPosition = TargetLocalPosition(tempStackItem.BeforeMe.transform.position);

        items.Add(tempGo);
        OnStackCountChanged?.Invoke(CurrentStackCount);
        
        tempStackItem.StartMovingWithScaling(startScale, targetScale);
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
    }
    private Vector3 TargetLocalPosition(Vector3 beforeMePos)
    {
        tempVec3 = container.position;
        tempVec3.y = beforeMePos.y + tileSize.y;

        return container.InverseTransformPoint(tempVec3);
    }
    public void AddItem(bool isCurrencyCallback = false)
    {
        if(!isCurrencyCallback && !dontUseCurrency)
            relatedCurrency.Value++;

        if (relatedCurrency.Value == items.Count)
            return;

        tempGo = PoolManager.fetch(yellowStackPrefabInfo.PoolName);
        tempGo.transform.parent = container;
        tempGo.transform.localPosition =  Getlocation(currentLocation);
        tempStackItem = tempGo.GetComponent<IStackItem>();
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);
        tempGo.name = "Flower Pot - " + "(" + currentLocation.x + ", " + currentLocation.z + ")";

        tempStackItem.YPositionOffset = tileSize.y;
        

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

    public void RemoveItem(bool isCurrencyCallback = false)
    {
        if (items.Count >= 1)
            RemoveItem(items[items.Count - 1], isCurrencyCallback);
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

        for (int i = items.Count - 1; i >= 0; i--)
            RemoveItem(items[i]);

        items.Clear();
    }  /// RemoveItem(Go)

    public void RemoveItem(GameObject go, bool isCurrencyCallback = false)
    {
        itemIndex = 0;
        tempStackItem = go.GetComponent<IStackItem>();
        if (items.Contains(go))
        {
            if(!isCurrencyCallback && !dontUseCurrency)
                relatedCurrency.Value--;

            itemIndex = items.IndexOf(go);
            currentLocation = tempStackItem.Location;
            currentStackSize = items.Count - itemIndex;

            items.Remove(go);
            OnStackCountChanged?.Invoke(CurrentStackCount);
            go.GetComponent<PoolObject>().release();

            for (int i = itemIndex; i < items.Count; i++)
            {

                tempStackItem = items[i].GetComponent<IStackItem>();
                tempStackItem.Location = currentLocation;
                tempStackItem.AttachedGameObject.name = "Flower Pot - " + "(" + currentLocation.x + ", " + currentLocation.z + ")";
                tempStackItem.YPositionOffset = tileSize.y;
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
        if (pos.x == 0)
            tempVec3.x = 0f;
        else
            tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);

        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        tempVec3 += holderOffset;
        return tempVec3;
    }
}

