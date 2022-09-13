using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackStackManager : MonoBehaviour
{
    public UnityEvent OnLogSpawned;
    public static BackStackManager ins;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Transform parentTransform;
    [SerializeField] Vector3 tileSize;
    [SerializeField] PoolInfo stackPoolInfo;
    [SerializeField] int currentStackSize = 0;
    
    Vector3Int currentPostion;
    Stack tempStack;
    Vector3 tempVec3;
    GameObject tempGO;
    List<GameObject> stackList = new List<GameObject>();
    int stackCount;
    public int CurrentStackSize { get => currentStackSize; set => currentStackSize = value; }

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        Health.OnDeathh += AddItem;
        relatedCurrency.OnValueChanged += OnCurrencyChangedValue;
        currentPostion = Vector3Int.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            AddItem();
    }
    int tempInt;
    private void OnCurrencyChangedValue(int count)
    {
        if(stackList.Count > count)
        {
            tempInt = stackList.Count - count;
            for (int i = tempInt - 1; i >= 0; i--)
                RemoveItem();
        }
        else if(stackList.Count < count)
        {
            tempInt = count - stackList.Count;
            for (int i = 0; i < tempInt; i++)
                AddItem();
        }
    }

    public void AddItem(GameObject go)
    {
        if (go.CompareTag("Log"))
        {
            for (int i = 0; i < go.GetComponent<LogController>().WoodCount; i++)
            {
                tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
                
                tempGO.transform.parent = parentTransform;
                tempGO.transform.localPosition = parentTransform.InverseTransformPoint(go.transform.position);
                tempStack = tempGO.GetComponent<Stack>();
                tempStack.Location = currentPostion;
                tempStack.TargetLocalPosition = Getlocation(currentPostion);
                stackList.Add(tempGO);
                relatedCurrency.Value++;
                CurrentStackSize++;
                currentPostion.x++;

                OnLogSpawned?.Invoke();

                tempGO.SetActive(true);
                tempStack.StartMoving();
                
                if (currentPostion.x >= stackSize.x)
                {
                    currentPostion.x = 0;
                    currentPostion.z++;
                    if (currentPostion.z >= stackSize.z)
                    {
                        currentPostion.z = 0;
                        currentPostion.y++;
                        if (currentPostion.y >= stackSize.y)
                            currentPostion.y = 0;
                    }
                }
            }
        }
        else if (go.CompareTag("Axe"))
        {
            
            tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
            tempGO.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            tempGO.transform.parent = parentTransform;

            tempGO.transform.localPosition = parentTransform.InverseTransformPoint(go.transform.position);
            tempStack = tempGO.GetComponent<Stack>();
            tempStack.Location = currentPostion;
            tempStack.TargetLocalPosition = Getlocation(currentPostion);
            stackList.Add(tempGO);
            relatedCurrency.Value++;
            OnLogSpawned?.Invoke();

            tempStack.StartMoving();
            tempGO.SetActive(true);

            CurrentStackSize++;
            currentPostion.x++;

            if (currentPostion.x >= stackSize.x)
            {
                currentPostion.x = 0;
                currentPostion.z++;
                if (currentPostion.z >= stackSize.z)
                {
                    currentPostion.z = 0;
                    currentPostion.y++;
                    if (currentPostion.y >= stackSize.y)
                        currentPostion.y = 0;
                }
            }


        }
    }

    public void AddItem()
    {
        //relatedCurrency.Value++;
        tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
        tempGO.transform.parent = parentTransform;
        tempGO.transform.localPosition = Vector3.zero + Vector3.forward * 2f;
        tempStack = tempGO.GetComponent<Stack>();
        tempStack.Location = currentPostion;
        tempStack.TargetLocalPosition = Getlocation(currentPostion);
        stackList.Add(tempGO);

        OnLogSpawned?.Invoke();

        tempStack.StartMoving();
        tempGO.SetActive(true);

        CurrentStackSize++;
        currentPostion.x++;
        
        if(currentPostion.x >= stackSize.x)
        {
            currentPostion.x = 0;
            currentPostion.z++;
            if(currentPostion.z >= stackSize.z)
            {
                currentPostion.z = 0;
                currentPostion.y++;
                if(currentPostion.y >= stackSize.y)
                {
                    currentPostion.y = 0;
                }
            }
        }
    }

    public void AddItemWithScaling(Vector3 from, Vector3 startScale, Vector3 targetScale)
    {
        relatedCurrency.Value++;
        tempGO = PoolManager.fetch(stackPoolInfo.PoolName);
        
        tempGO.transform.parent = parentTransform;
        tempGO.transform.localPosition = parentTransform.InverseTransformPoint(from);
        tempGO.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        
        tempStack = tempGO.GetComponent<Stack>();
        tempStack.Location = currentPostion;
        tempStack.TargetLocalPosition = Getlocation(currentPostion);
        stackList.Add(tempGO);

        OnLogSpawned?.Invoke();
        
        tempStack.StartMovingWithScaling(startScale, targetScale);
        tempGO.SetActive(true);

        CurrentStackSize++;
        currentPostion.x++;

        if (currentPostion.x >= stackSize.x)
        {
            currentPostion.x = 0;
            currentPostion.z++;
            if (currentPostion.z >= stackSize.z)
            {
                currentPostion.z = 0;
                currentPostion.y++;
                if (currentPostion.y >= stackSize.y)
                {
                    currentPostion.y = 0;
                }
            }
        }
    }

    private void RemoveItem(bool fromCurrencyValueChangedAction = false)
    {
        if (stackList.Count >= 1)
        {
            if (fromCurrencyValueChangedAction)
                relatedCurrency.Value--;

            tempGO = stackList[stackList.Count - 1];
            currentPostion = tempGO.GetComponent<Stack>().Location;
            stackList.Remove(tempGO);
            tempGO.GetComponent<PoolObject>().release();
            CurrentStackSize--;
        }
    }
    
    public void RemoveAll()
    {
        stackCount = stackList.Count;
        for (int i = 0; i < stackCount; i++)
            RemoveItem(true);
    }

    private Vector3 Getlocation(Vector3Int pos)
    {
        tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);
        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        return tempVec3;
    }

    private void OnApplicationQuit()
    {
        relatedCurrency.Value = 0;
    }
}
