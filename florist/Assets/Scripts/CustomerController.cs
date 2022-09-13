using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class CustomerController : MonoBehaviour, IPoolObject
{
    public Action<CustomerState> OnCustomerStateChanged;
    public CustomerState state;
    public Transform ReturnPositionTransform;
    private Transform registerPositionTransform;
    public float stopDistanceToStand;
    [SerializeField] int shopListMax;
    [SerializeField] bool isRich;
    [SerializeField] bool startMoving;
    [SerializeField] bool isMapChanging = false;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] FrontStackUp frontStackUp;
    [SerializeField] TMP_Text CustomerTopText;
    [SerializeField] string zoneId;
    [SerializeField] GameObject meshGo;

    Vector2 tempVec2;
    Vector3 tempVec3;
    FlowerTypeSC tempFlowerType;
    bool isDestinationReached;
    bool isReturning;
    bool isReadyToPay;
    bool isExchangeSuccesful;
    Stand targetStand;
    int failsafe = 0;
    int shopListCount = 0;
    int currentShoplistCount = 0;
    Vector3 randomPosAroundStand;
    Vector3 tempVec;
    public bool IsAllBought => CurrentShoplistCount >= shopListCount;
    public CustomerState State
    {
        get => state;

        set
        {
            if (state != value)
            {
                state = value;
                OnCustomerStateChanged?.Invoke(state);
            }
        }
    }

    public int CurrentShoplistCount
    {
        get => currentShoplistCount;
        set
        {
            currentShoplistCount = value;
            UpdateUI();
        }
    }
    int failSafe;
    public bool IsMapChanging { get => isMapChanging; set => isMapChanging = value; }
    public string ZoneId { get => zoneId; set => zoneId = value; }
    public Transform RegisterPositionTransform
    {
        get
        {
            failSafe = 0;
            registerPositionTransform = RegisterController.ins.GetActiveRegisterTransform();


            if (registerPositionTransform == null)
            {
                registerPositionTransform = RegisterController.ins.transform;
            }

            /*while (registerPositionTransform == null && failSafe <= 100)
            {
                failSafe++;
                registerPositionTransform = RegisterController.ins.GetActiveRegisterTransform();
            }*/
            return registerPositionTransform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        LevelComponent.OnPlayerLeaveIdleGround += StartZoneChanging;
    }

    public Stand tempStand;
    public void StartZoneChanging()
    {
        if (isReturning || targetStand == null)
        {
            EnemySpawnManager.RemoveFromActiveEnemyList(gameObject);
            GetComponent<PoolObject>().release();
        }
        meshGo.SetActive(false);
        IsMapChanging = true;
        agent.enabled = false; // Navmesh stop
        transform.parent = InfiniteRoad.ins.GetNextZone().transform;
        tempStand = FindStandTwin();

        if (tempStand != null)
        {
            targetStand = tempStand;   // change tempStand
            WarpToDestination(targetStand.transform.position); // randomPosAroundStand değişmeyecek kaydını tut
            meshGo.SetActive(true);
            ReturnPositionTransform = InfiniteRoad.ins.GetNextZone().Exit;// change return position
            ZoneId = InfiniteRoad.ins.GetNextZone().ZoneId; // change active zone id

            EndZoneChanging();
            SetDestination(targetStand.transform.position, true);
        }
    }
    public void EndZoneChanging()
    {
        IsMapChanging = false;
        agent.enabled = true; // Navmesh start
        
    }
    private Stand FindStandTwin()
    {
        for (int i = 0; i < Stand.ActiveStands.Count; i++)
        {
            if (targetStand == null)
                continue;

            if(Stand.ActiveStands[i] == null)
                Debug.Log("Stand.ActiveStands[i] null   i : " + i);

            if (targetStand != Stand.ActiveStands[i] && Stand.ActiveStands[i].PrefId == targetStand.PrefId)
                return Stand.ActiveStands[i];
        }

        return null;
    }
    
    private void UpdateUI()
    {
        if (CustomerTopText != null)
            CustomerTopText.text = CurrentShoplistCount + "/" + shopListCount;
    }
    private void RandomShopList()
    {
        if (!isRich)
            shopListCount = UnityEngine.Random.Range(1, shopListMax);
        else
            shopListCount = shopListMax;

        UpdateUI();
    }
    private void Update()
    {
        if (IsMapChanging)
            return;

        StateCheck();
        if (isDestinationReached && !isReturning && !isReadyToPay)
        {
            isExchangeSuccesful = targetStand.TakeFlower(frontStackUp);

            if (isExchangeSuccesful)
            {
                CurrentShoplistCount++;
                if (IsAllBought)
                {
                    isReadyToPay = true;
                    SetDestination(RegisterPositionTransform.position);     
                }
            }
        }
        else if (isDestinationReached && isReadyToPay && !isReturning)
        {
            Pay();
            isReturning = true;
            SetDestination(ReturnPositionTransform.position);
        }
        else if(isDestinationReached && isReturning && isReadyToPay)
        {
            EnemySpawnManager.RemoveFromActiveEnemyList(gameObject);
            GetComponent<PoolObject>().release();
        }
    }
    
    private void Pay()
    {
        for (int i = 0; i < frontStackUp.CurrentStackCount; i++)
        {
            tempFlowerType =  frontStackUp.items[i].GetComponent<FlowerType>().Type;
            for (int k = 0; k < tempFlowerType.SellPrice; k++)
            {
                RegisterPositionTransform.GetComponent<RegisterController>().AddWithScaling(transform.position, Vector3.zero, Vector3.one); 
            }
        }
    }
    private void StateCheck()
    {
        if (!agent.pathPending && (agent.remainingDistance <= agent.stoppingDistance) &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            SetState(CustomerState.Idle);
            isDestinationReached = true;
        }
        else
            SetState(CustomerState.Running);
    }

    private void SetState(CustomerState customerState)
    {
        if (state != customerState)
            state = customerState;
    }

    public void StartMoving()
    {
        targetStand = RandomStand();
        transform.parent = InfiniteRoad.ins.ActiveZone.transform;
        zoneId = InfiniteRoad.ins.ActiveZoneId;

        RandomShopList();
        
        if (targetStand != null)
            SetDestination(RandomStand().transform.position);
    }

    
    private void WarpToDestination(Vector3 pos)
    {
        if(pos != null)
        {
            if (agent.Warp(pos + randomPosAroundStand + tempVec))
                Debug.Log("teleport success.");
        }
    }
    private void SetDestination(Vector3 pos, bool useOldRandomPosAroundStand = false)
    {
        if (pos != null)
        {
            if(!useOldRandomPosAroundStand && randomPosAroundStand != null)
                randomPosAroundStand = GetRandomPoisitonAroundStand();

            agent.SetDestination(pos + randomPosAroundStand);

            isDestinationReached = false;
        }
    }
    
    private Vector3 GetRandomPoisitonAroundStand()
    {
        tempVec2 = UnityEngine.Random.insideUnitCircle * stopDistanceToStand;
        tempVec3.x += tempVec2.x;
        tempVec3.z += tempVec2.y;

        return tempVec3;
    }
    private Stand RandomStand()
    {
        failsafe = 100;
        while (Stand.ActiveStands.Count > 0 && failsafe > 0)
        {
            targetStand = Stand.ActiveStands[UnityEngine.Random.Range(0, Stand.ActiveStands.Count)];

            if (targetStand.GetComponent<ZoneIdentity>().Id == InfiniteRoad.ins.ActiveZoneId)
                return targetStand;

            failsafe--;
        }
        
            return null;
    }

    #region Pool
    public void clearForRelease()
    {
        isDestinationReached = false;
        isReturning = false;
        isExchangeSuccesful = false;
        isReadyToPay = false;
        targetStand = null;
        frontStackUp.RemoveAll();
        CurrentShoplistCount = 0;
        shopListCount = 0;
        IsMapChanging = false;
        transform.parent = null;
    }

    public void resetForRotate()
    {
        isDestinationReached = false;
        isReturning = false;
        isExchangeSuccesful = false;
        isReadyToPay = false;
        targetStand = null;
        frontStackUp.RemoveAll();
        CurrentShoplistCount = 0;
        shopListCount = 0;
        IsMapChanging = false;
        transform.parent = null;
    }

    public void OnCreate()
    {
        
    }

    #endregion
}
public enum CustomerState
{
    Idle,
    Running
}
