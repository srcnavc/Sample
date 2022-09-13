using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    public static Action<SeedController, float> OnAllStagesUpdated;
    public static Action<SeedController> OnBloomed;
    public static Action<SeedController> OnFlowerTaken; // NOT SET YET
    public static Action<SeedController> OnGrowingStarted;
    public static Action<SeedController> OnSeedEnabled;
    public static Action OnRemaininCostChanged;
    public string id;
    public bool IsActive;
    public string PrefId => GetComponentInParent<FieldController>().PrefId + "_" + id + "_Seed";
    [SerializeField] bool isFlowerReady = false;
    [SerializeField] GrowStage[] stages;
    [SerializeField] float bloomTimeInSecond = 1f;
    [SerializeField] int buyCost;
    [SerializeField] int remainingCost;
    [SerializeField] CurrencySC relatedCurrency;
    [SerializeField] FlowerTypeSC flowerType;

    
    float startTime;
    bool isActive;
    [Serializable]
    public class GrowStage
    {
        [HideInInspector] public string name;
        [SerializeField] float percent = -1f;
        [SerializeField] public SkinnedMeshRenderer smr;
        [SerializeField] bool isComplete;
        public bool IsComplete
        {
            get
            {
                if (percent >= 100f)
                {
                    if (!isComplete)
                        isComplete = true;
                }
                else
                {
                    if (isComplete)
                        isComplete = false;
                }

                return isComplete;
            }
        }
        public float Percent
        {
            get => percent;

            set
            {
                if(percent != value)
                {
                    percent = value;
                    smr.SetBlendShapeWeight(0, value);
                }
            }
        }
    }

    private float RemainingPercentage => (1 - ((startTime + BloomTimeInSecond - Time.time) / BloomTimeInSecond)) * 100f;

    public float BloomTimeInSecond { get => bloomTimeInSecond; set => bloomTimeInSecond = value; }

    public int Cost => buyCost;

    public CurrencySC RelatedCurrency => relatedCurrency;

    public int RemainingCost { get => remainingCost; set => SetRemainingCost(value); }

    private void SetRemainingCost(int remain)
    {
        remainingCost = remain;
        OnRemaininCostChanged?.Invoke();
    }
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(PrefId))
            PlayerPrefs.SetInt(PrefId, 0);

        if (!PlayerPrefs.HasKey(PrefId + "_RemaininCost"))
            PlayerPrefs.SetInt(PrefId + "_RemaininCost", Cost);

        RemainingCost = PlayerPrefs.GetInt(PrefId + "_RemaininCost");
        ChangeFlowerColorByType();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", Cost);
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt(PrefId + "_RemaininCost", Cost);
    }
    private void Start()
    {
        OnAllStagesUpdated += OnAllStagesUpdate;
        OnBloomed += OnBloom;
        OnFlowerTaken += OnFlowerTakenn;
        OnGrowingStarted += OnGrowingStart;
        OnSeedEnabled += OnSeedEnable;
    }

    private void OnDestroy()
    {
        OnAllStagesUpdated -= OnAllStagesUpdate;
        OnBloomed -= OnBloom;
        OnFlowerTaken -= OnFlowerTakenn;
        OnGrowingStarted -= OnGrowingStart;
        OnSeedEnabled -= OnSeedEnable;
    }

    private void OnAllStagesUpdate(SeedController seed, float percent)
    {
        if (seed.PrefId.Equals(PrefId) && seed != this)
            SetAllStagesPercents(percent, true);
    }

    private void OnBloom(SeedController seed)
    {
        if (seed.PrefId.Equals(PrefId) && seed != this)
            Bloom(true);
    }

    private void OnFlowerTakenn(SeedController seed)
    {
        if (seed.PrefId.Equals(PrefId) && seed != this)
            GetFlower();
    }

    private void OnGrowingStart(SeedController seed)
    {
        if (seed.PrefId.Equals(PrefId) && seed != this)
            StartGrowning(true);
    }

    private void OnSeedEnable(SeedController seed)
    {
        if (seed.PrefId.Equals(PrefId) && seed != this)
            SetSeedEnable(true);
    }

    public void SetSeedEnable(bool calledByEvent = false)
    {
        IsActive = true;

        if (!calledByEvent)
            OnSeedEnabled?.Invoke(this);
    }

    void LateUpdate()
    {
        if (!isFlowerReady && IsActive)
        {
            if (startTime + BloomTimeInSecond <= Time.time)
                Bloom();
            else 
                UpdateBlendShape(RemainingPercentage);
        }
        
    }

    private void UpdateBlendShape(float percent)
    {
        if(stages.Length > 0)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                if (!stages[i].IsComplete)
                {
                    stages[i].Percent = (percent * stages.Length) - (i * 100f);
                    return;
                }
            }
        }
    }

    private void SetAllStagesPercents(float percent, bool calledByEvent = false)
    {
        for (int i = 0; i < stages.Length; i++)
            stages[i].Percent = percent;

        if(!calledByEvent)
            OnAllStagesUpdated?.Invoke(this, percent);
    }

    private void Bloom(bool calledByEvent = false)
    {
        isFlowerReady = true;
        SetAllStagesPercents(100);

        if (!calledByEvent)
            OnBloomed?.Invoke(this);
    }

    public void GetFlower()
    {
        if (isFlowerReady)
        {
            FrontStackUp.ins.AddItemWithScaling(gameObject, transform.position, Vector3.one, Vector3.one);
            StartGrowning();
        }
    }

    public void StartGrowning(bool calledByEvent = false)
    {
        startTime = Time.time;
        SetAllStagesPercents(0);
        isFlowerReady = false;

        if (!calledByEvent)
            OnGrowingStarted?.Invoke(this);
    }

    private void ChangeFlowerColorByType()
    {
        if (flowerType == null)
            return;

        FlowerBlendShapeLocation[] flowerBlendShapes = GetComponentsInChildren<FlowerBlendShapeLocation>(true);

        for (int i = 0; i < flowerBlendShapes.Length; i++)
            flowerBlendShapes[i].gameObject.SetActive(false);

        switch (flowerType.Color)
        {
            case FlowerColor.Blue:
                stages[0].smr = flowerBlendShapes[0].skinnedMesh1;
                stages[1].smr = flowerBlendShapes[0].skinnedMesh2;
                flowerBlendShapes[0].gameObject.SetActive(true);
                break;
            case FlowerColor.Red:
                stages[0].smr = flowerBlendShapes[1].skinnedMesh1;
                stages[1].smr = flowerBlendShapes[1].skinnedMesh2;
                flowerBlendShapes[1].gameObject.SetActive(true);
                break;
            case FlowerColor.Yellow:
                stages[0].smr = flowerBlendShapes[2].skinnedMesh1;
                stages[1].smr = flowerBlendShapes[2].skinnedMesh2;
                flowerBlendShapes[2].gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
    private void OnValidate()
    {
        for (int i = 0; i < stages.Length; i++)
            stages[i].name = "Stage " + (i+1);

        ChangeFlowerColorByType();
    }
}
