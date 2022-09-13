using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterPile : MonoBehaviour
{
    private static Action<RegisterPile, GameObject, ChangeType> OnPileCountChanged;
    public static RegisterPile ins;
    public Action OnStackIncrease;
    [SerializeField] Transform parent;
    [SerializeField] Transform container;
    [SerializeField] Transform container2;
    [SerializeField] Transform container3;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Vector3Int currentLocation;
    [SerializeField] Vector3 tileSize;
    [SerializeField] int currentStackSize;
    [SerializeField] PoolInfo stackPrefabInfo;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] bool dontUseCurrency;
    [SerializeField] Vector3 holderOffset;
    [SerializeField] string saveId;
    public List<GameObject> items = new List<GameObject>();

    public int CurrentStackCount => items.Count;
    Vector3 tempVec3;
    GameObject tempGo;
    IStackItem tempStackItem;
    int itemIndex = 0;
    int tempInt;
    public string PrefId => SaveId + "_Register";
    public string SaveId { get => saveId; set => saveId = value; }
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!dontUseCurrency)
        {
            relatedCurrency.OnValueChanged += OnCurrencyChangedValue;
            tempInt = relatedCurrency.Value;
            for (int i = 0; i < tempInt; i++)
                AddItemWithScaling(transform.position, Vector3.one, Vector3.one, true);
        }
        else
            currentLocation = Vector3Int.zero;

        OnPileCountChanged += OnListCountChanged;
    }

    private void OnApplicationQuit()
    {
        OnPileCountChanged -= OnListCountChanged;
    }
    private void Update()
    {
        if (ins == this && Input.GetKeyDown(KeyCode.L))
            AddItemWithScaling(transform.position, Vector3.one, Vector3.one);
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
                AddItemWithScaling(transform.position, Vector3.one, Vector3.one, true);
        }*/

    }
    private void OnListCountChanged(RegisterPile callOwner, GameObject go, ChangeType type)
    {
        if (PrefId == callOwner.PrefId && callOwner != this)
        {
            if (type == ChangeType.Add)
            {
                AddItem(true);
            }
            else if (type == ChangeType.Remove)
            {
                RemoveItem(true);
            }
        }
    }

    private enum ChangeType
    {
        Add,
        Remove
    }

    public GameObject GetLastStackItem()
    {
        if (items.Count > 0)
            return items[items.Count - 1];
        else
            return null;
    }

    public void AddItemWithScaling(Vector3 from, Vector3 startScale, Vector3 targetScale, bool isCurrencyCallback = false)
    {
        

        tempGo = PoolManager.fetch(stackPrefabInfo.PoolName);


        //tempGo.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        tempGo.name = "Flower Pot - " + "(" + currentLocation.y + ", " + currentLocation.x + ")";

        tempStackItem = tempGo.GetComponent<IStackItem>();
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);

        //Debug.Log(currentLocation);
        

        tempStackItem.YPositionOffset = tileSize.y;
        //tempGo.transform.position = from; //+ Getlocation(currentLocation);

        if (items.Count >= stackSize.x)
        {
            if (currentLocation.x == 0)
                tempGo.transform.parent = container;
            else if (currentLocation.x == 1)
                tempGo.transform.parent = container2;
            else if (currentLocation.x == 2)
                tempGo.transform.parent = container3;


            tempStackItem.BeforeMe = items[items.Count - stackSize.x];
            tempGo.transform.localRotation = tempGo.transform.parent.localRotation;//items[items.Count - stackSize.x].transform.localRotation;
        }
        else
        {
            if (currentLocation.x == 0)
            {
                tempGo.transform.parent = container;
                tempStackItem.BeforeMe = container.gameObject;
                tempGo.transform.localRotation = container.localRotation;
            }
            else if (currentLocation.x == 1)
            {
                tempGo.transform.parent = container2;
                tempStackItem.BeforeMe = container2.gameObject;
                tempGo.transform.localRotation = container2.localRotation;
            }
            else if (currentLocation.x == 2)
            {
                tempGo.transform.parent = container3;
                tempStackItem.BeforeMe = container3.gameObject;
                tempGo.transform.localRotation = container3.localRotation;
            }
        }

        tempGo.transform.localPosition = tempGo.transform.parent.InverseTransformPoint(from);
        tempStackItem.TargetLocalPosition = TargetLocalPosition(tempStackItem.BeforeMe.transform.position);


        items.Add(tempGo);

        if (!isCurrencyCallback && !dontUseCurrency)
            relatedCurrency.Value++;

        OnStackIncrease?.Invoke();

        if (!isCurrencyCallback)
            OnPileCountChanged?.Invoke(this, tempGo, ChangeType.Add);
        
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
        tempVec3 = parent.position;
        tempVec3.y = beforeMePos.y + tileSize.y;

        return parent.InverseTransformPoint(tempVec3);
    }
    public void AddItem(bool isCurrencyCallback = false)
    {
        if (!isCurrencyCallback && !dontUseCurrency)
            relatedCurrency.Value++;

        tempGo = PoolManager.fetch(stackPrefabInfo.PoolName);
        tempGo.transform.parent = container;
        tempGo.transform.localPosition = Getlocation(currentLocation);
        tempGo.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        tempStackItem = tempGo.GetComponent<IStackItem>();
        tempStackItem.Location = new Vector3Int(currentLocation.x, currentLocation.y, currentLocation.z);
        tempGo.name = "Money - " + "(" + currentLocation.x + ", " + currentLocation.y + ")";

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
            

            itemIndex = items.IndexOf(go);
            currentLocation = tempStackItem.Location;
            currentStackSize = items.Count - itemIndex;

            items.Remove(go);

            if (!isCurrencyCallback && !dontUseCurrency)
                relatedCurrency.Value--;
            
            go.GetComponent<PoolObject>().release();

            if (!isCurrencyCallback)
                OnPileCountChanged?.Invoke(this, go, ChangeType.Remove);

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
        tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);
        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        tempVec3 += holderOffset;
        return tempVec3;
    }
}
