using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager ins;
    public List<EnemySpawner> spawners = new List<EnemySpawner>();
    private bool isEnabled;
    [SerializeField] float delay;
    [SerializeField] int customerLimit;
    float timer = 0f;
    private static List<GameObject> enemyList = new List<GameObject>();
    GameObject tempGo;
    bool isCalledFirstTime = false;
    public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    private void Start()
    {
        if (InfiniteRoad.ins.ActiveZone != null)
            IsEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsEnabled && GameStateManager.GetState() == GameState.play)//if (GameStateManager.GetState() == GameState.play)
        {
            if (enemyList.Count < (customerLimit * Stand.ActiveStands.Count / 2) && timer + (delay / Stand.ActiveStands.Count) <= Time.time && Stand.ActiveStands.Count > 0)
                SpawnWithDelay();
        }
    }
    
    private void LateUpdate()
    {
        if (!isCalledFirstTime)
            isCalledFirstTime = true;
    }
    private void SpawnWithDelay()
    {
        //Debug.Log("customer count : " + enemyList.Count + "  d : " + (customerLimit * Stand.ActiveStands.Count) + "  a : " + Stand.ActiveStands.Count);
        timer = Time.time;
        tempGo = spawners[Random.Range(0, spawners.Count)].SpawnFromPool(InfiniteRoad.ins.ActiveZone.Entrance);
        tempGo.GetComponent<CustomerController>().StartMoving();
        tempGo.GetComponent<CustomerController>().ReturnPositionTransform = InfiniteRoad.ins.ActiveZone.Exit;
        tempGo.name = tempGo.name + "_" + Time.time;
        enemyList.Add(tempGo);
    }

    public static void RemoveFromActiveEnemyList(GameObject go)
    {
        if(enemyList.Contains(go))
            enemyList.Remove(go);
    }
}
