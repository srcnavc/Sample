using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LogPile : MonoBehaviour
{
    private static Action<LogPile, GameObject, ChangeType> OnPileCountChanged;
    private static Action<LogPile> OnSavesLoaded;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] Vector3Int stackSize;
    [SerializeField] Transform parentTransform;
    [SerializeField] Vector3 tileSize;
    [SerializeField] PoolInfo stackPoolInfo;
    [SerializeField] PoolInfo blueStackPrefabInfo;
    [SerializeField] PoolInfo redStackPrefabInfo;
    [SerializeField] PoolInfo yellowStackPrefabInfo;
    [SerializeField] int currentStackSize = 0;
    [SerializeField] float exchangeSpeed;
    [SerializeField] bool limitThePile = false;
    [SerializeField] bool saveData = false;
    [SerializeField] string saveId;
    [SerializeField] bool isSavesLoaded = false;
    [SerializeField] List<GameObject> stackList = new List<GameObject>();
    
    bool isPlayerInExchangeArea = false;

    GameObject player;
    Vector3Int currentPostion;
    Stack tempStack;
    Vector3 tempVec3;
    GameObject tempGO;
    int tempInt;
    string tempPoolName;
    int stackCount;
    FlowerTypeSC tempFlowerTypeSc;
    public bool IsLimitReached { get => !(stackList.Count < SizeLimit); }
    
    public int SizeLimit => stackSize.x * stackSize.y * stackSize.z;
    public int CurrentStackSize { get => currentStackSize; set => currentStackSize = value; }
    public bool AddItemToPile
    {
        get => isPlayerInExchangeArea; 
        set
        {
            isPlayerInExchangeArea = value;
            if (isPlayerInExchangeArea)
                StartCoroutine(AddItemToPileFromPlayer());
            else
                StopCoroutine(AddItemToPileFromPlayer());
        }
    }

    public bool AddItemToPlayer
    {
        get => isPlayerInExchangeArea; set
        {
            isPlayerInExchangeArea = value;
            if (!isPlayerInExchangeArea)
                StopCoroutine(AddItemToPlayerFromPile());
            else
                StartCoroutine(AddItemToPlayerFromPile());
        }
    }

    public string PrefId  => SaveId + "_LogPile";

    public string SaveId { get => saveId; set => saveId = value; }

    private enum ChangeType
    {
        Add,
        Remove
    }

    private void Start()
    {
        OnPileCountChanged += OnListCountChanged;
        OnSavesLoaded += OnsavesLoaded;
        if (relatedCurrency != null)
            relatedCurrency.OnValueChanged += OnValueChanged;

        player = GameObject.FindGameObjectWithTag("Player");
        currentPostion = Vector3Int.zero;

        if(!isSavesLoaded)
            LoadStackData();
        
        /*for (int i = 0; i < relatedCurrency.Value; i++)
            AddItem();*/
    }

    private void OnApplicationQuit()
    {
        if (saveData)
            SaveStackArray();

        OnPileCountChanged -= OnListCountChanged;
        OnSavesLoaded -= OnsavesLoaded;
    }

    private void OnApplicationPause(bool pause)
    {
        if (saveData)
            SaveStackArray();

        OnPileCountChanged -= OnListCountChanged;
        OnSavesLoaded -= OnsavesLoaded;
    }
    private void OnsavesLoaded(LogPile callOwner)
    {
        if(PrefId == callOwner.PrefId && callOwner != this)
        {
            if (!isSavesLoaded)
                LoadStackData();
        }
    }

    private void OnValueChanged(int count)
    {
        /*if (stackList.Count > count)
        {
            tempInt = stackList.Count - count;
            for (int i = tempInt - 1; i >= 0; i--)
                RemoveItem();
        }
        else if (stackList.Count < count)
        {
            tempInt = count - stackList.Count;
            for (int i = 0; i < tempInt; i++)
                AddItem();
        }*/
    }

    private void OnListCountChanged(LogPile callOwner, GameObject go, ChangeType type)
    {
        if(PrefId == callOwner.PrefId && callOwner != this)
        {
            if (type == ChangeType.Add)
            {
                AddItem(go, true);
            }
            else if (type == ChangeType.Remove)
            {
                RemoveItem(true);
            }
        }
    }
    public void AddItem(GameObject go)
    {
        AddItem(go, false);
    }
    private void AddItem(GameObject go, bool isCallBack = false)
    {
        
        if (go != null && go.GetComponent<FlowerType>() != null)
            tempGO = PoolManager.fetch(GetPoolName(go.GetComponent<FlowerType>().Type));
        else
            tempGO = PoolManager.fetch(stackPoolInfo.PoolName);

        tempGO.transform.parent = parentTransform;
        tempGO.transform.localPosition = Getlocation(currentPostion);
        tempGO.transform.rotation = parentTransform.rotation;
        tempStack = tempGO.GetComponent<Stack>();
        tempStack.Location = currentPostion;
        tempStack.TargetLocalPosition = Getlocation(currentPostion);
        stackList.Add(tempGO);
        if(!isCallBack)
            OnPileCountChanged?.Invoke(this, tempGO, ChangeType.Add);

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

    private void SaveStackArray()
    {
        string rawData = "";
        if (stackList.Count > 0)
        {
            for (int i = 0; i < stackList.Count; i++)
            {
                switch (stackList[i].GetComponent<FlowerType>().Type.Color)
                {
                    case FlowerColor.Blue:
                        rawData += "B";
                        break;
                    case FlowerColor.Red:
                        rawData += "R";
                        break;
                    case FlowerColor.Yellow:
                        rawData += "Y";
                        break;
                    default:
                        Debug.Log("Unknown Flower Color.");
                        break;
                }
            }
        }
        else
            rawData = "0";
        

        //Debug.Log(rawData + "  " + gameObject.name + "  " + transform.position);
        PlayerPrefs.SetString(PrefId, rawData);
    }

    private void LoadStackData()
    {
        string rawData = PlayerPrefs.GetString(PrefId);
        
        if(rawData != "" && rawData != null)
        {
            char[] charList = rawData.ToCharArray();
            if(charList[0] != '0')
            {
                for (int i = 0; i < charList.Length; i++)
                {
                    switch (charList[i])
                    {
                        case 'B':
                            tempGO = blueStackPrefabInfo.Prefab;
                            break;
                        case 'R':
                            tempGO = redStackPrefabInfo.Prefab;
                            break;
                        case 'Y':
                            tempGO = yellowStackPrefabInfo.Prefab;
                            break;
                        default:
                            break;
                    }
                    //Debug.Log(rawData + "  " + gameObject.name + "  " + transform.position);
                    AddItem(tempGO, true);
                }
            }
        }

        isSavesLoaded = true;
       // OnSavesLoaded?.Invoke(this);
    }
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
    

    private IEnumerator AddItemToPileFromPlayer()
    {
        CurrencyContainer tempCon = player.GetComponent<CurrencyContainer>();
        
        while(AddItemToPile && FrontStackUp.ins.CurrentStackCount > 0 && !IsLimitReached) // tempCon.GetCurrencyValue(relatedCurrency.Id) > 0
        {
            if (FrontStackUp.ins.GetLastStackItem() != null)
                tempGO = PoolManager.fetch(GetPoolName(FrontStackUp.ins.GetLastStackItem().GetComponent<FlowerType>().Type));
            
            tempGO.transform.parent = parentTransform;
            if(FrontStackUp.ins.GetLastStackItem() != null)
                tempGO.transform.localPosition = parentTransform.InverseTransformPoint(FrontStackUp.ins.GetLastStackItem().transform.position);
            else
                tempGO.transform.localPosition = parentTransform.InverseTransformPoint(player.transform.position);
            
            tempGO.transform.rotation = parentTransform.rotation;
            tempStack = tempGO.GetComponent<Stack>();
            tempStack.Location = currentPostion;
            
            tempStack.TargetLocalPosition = Getlocation(currentPostion);


            stackList.Add(tempGO);

            OnPileCountChanged?.Invoke(this, tempGO, ChangeType.Add);

            tempStack.StartMovingWithScaling(Vector3.one * 2.5f, Vector3.one);
            tempGO.SetActive(true);

            relatedCurrency.Value++;
            //tempCon.DecreaseCurrency(relatedCurrency.Id, 1);
            FrontStackUp.ins.RemoveItem();

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

            yield return new WaitForSeconds(exchangeSpeed);
        }

        if(IsLimitReached)
            StopCoroutine(AddItemToPileFromPlayer());
    }

    private IEnumerator AddItemToPlayerFromPile()
    {
        while (AddItemToPlayer && stackList.Count > 0) // relatedCurrency.Value > 0 
        {
            
            FrontStackUp.ins.AddItemWithScaling(stackList[stackList.Count - 1], stackList[stackList.Count - 1].transform.position, Vector3.one * 0.4f, Vector3.one);
            //BackStackManager.ins.AddItemWithScaling(stackList[stackList.Count - 1].transform.position, Vector3.one, Vector3.one);
            relatedCurrency.Value--;
            RemoveItem();
            
            yield return new WaitForSeconds(exchangeSpeed);
        }
    }

    public FlowerTypeSC AddItemToCustomer(FrontStackUp frontStackUp)
    {
        if(relatedCurrency.Value > 0 && stackList.Count > 0)
        {
            tempFlowerTypeSc = stackList[stackList.Count - 1].GetComponent<FlowerType>().Type;
            frontStackUp.AddItemWithScaling(stackList[stackList.Count - 1], stackList[stackList.Count - 1].transform.position, Vector3.one, Vector3.one);
            relatedCurrency.Value--;
            RemoveItem();
            return tempFlowerTypeSc;
        }
        else
        {
            return null;
        }
    }

    private void RemoveItem(bool isCallBack = false)
    {
        if (stackList.Count >= 1)
        {
            tempGO = stackList[stackList.Count - 1];
            currentPostion = tempGO.GetComponent<Stack>().Location;
            stackList.Remove(tempGO);
            if (!isCallBack)
                OnPileCountChanged?.Invoke(this, tempGO, ChangeType.Remove);
            tempGO.GetComponent<PoolObject>().release();
            CurrentStackSize--;
        }
    }
    private Vector3 TargetLocalPosition(Vector3 beforeMePos)
    {
        tempVec3 = parentTransform.position;
        tempVec3.y = beforeMePos.y + tileSize.y;

        return parentTransform.InverseTransformPoint(tempVec3);
    }
    private Vector3 Getlocation(Vector3Int pos)
    {
        tempVec3.x = (tileSize.x / 2f) * (pos.x + 1);
        tempVec3.z = (tileSize.z / 2f) * (pos.z - 1);
        tempVec3.y = (tileSize.y / 2f) * (pos.y + 1);
        return tempVec3;
    }
}
