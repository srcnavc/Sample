using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FIMSpace.FTail;

public class FlowerPotController : MonoBehaviour, IStackItem, IPoolObject
{
    [SerializeField] string Tag;
    [SerializeField] StackMovement stackMove;
    [SerializeField] TailAnimator2 tailAnim;
    public Action<Vector3Int> OnLocationChanged;
    float distanceBetweenZ = 1f;
    float distanceBetweenY = 1f;
    [SerializeField] float followSpeed = 1f;
    [SerializeField] GameObject beforeMe;
    [SerializeField] Vector3Int location;
    [SerializeField] Catapult catapult;
    [SerializeField] bool isActive = false;
    bool isRotationSetted = false;
    Vector3 tempVec;
    Vector3 refVector3;
    Vector3 targetLocalPosition;
    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    public GameObject BeforeMe { get => beforeMe; set => beforeMe = value; }
    public Vector3Int Location
    {
        get => location;

        set
        {
            if (location != value)
            {
                location = value;
                OnLocationChange();
            }
        }
    }


    public GameObject AttachedGameObject { get => gameObject; }
    public Vector3 PositionOffset { get => tempVec; set => tempVec = value; }
    public float ZPositionOffset { get => distanceBetweenZ; set => distanceBetweenZ = value; }
    public float YPositionOffset { get => distanceBetweenY; set => distanceBetweenY = value; }
    public bool IsActive { get => isActive; set => isActive = value; }
    public float FollowSpeed { get => followSpeed; }//Variables.GetFloat("FollowSpeed"); }
    public Vector3 TargetLocalPosition { get => targetLocalPosition; set => targetLocalPosition = value; }

    #region Pool
    public void clearForRelease()
    {
        ResetParams();
    }

    public void OnCreate()
    {

    }

    public void resetForRotate()
    {
        //ResetParams();
    }
    #endregion

    private void OnLocationChange()
    {
        OnLocationChanged?.Invoke(location);
    }


    public void ResetParams()
    {
        IsActive = false;
        BeforeMe = null;
        Location = Vector3Int.zero;
        gameObject.name = "Flower Pot";
        isRotationSetted = false;
        transform.position = Vector3.zero;
        transform.parent = null;
        if (tailAnim != null)
            tailAnim.enabled = false;
    }

    public void SetParentNull()
    {
        transform.parent = null;
    }

    public void Scatter()
    {
        IsActive = false;
        catapult.Throw(gameObject);
        //GetComponent<PoolObject>().release();
    }



    void Update()
    {
        if (BeforeMe != null && beforeMe.activeSelf && IsActive)
        {
            if (!BeforeMe.CompareTag("FrontStackIndicator"))
            {
                Vector3 tempvec = transform.position;
                Vector3 tempvec2 = BeforeMe.transform.position;

                transform.position = Vector3.SmoothDamp(tempvec, tempvec2,
                    ref refVector3,
                FollowSpeed * Time.deltaTime);

                transform.position = new Vector3(transform.position.x, tempvec2.y + YPositionOffset,
                transform.position.z);

            }
            else
            {

                Vector3 tempvec = transform.position;
                Vector3 tempvec2 = BeforeMe.transform.position;


                transform.position = Vector3.SmoothDamp(tempvec, tempvec2, ref refVector3,
                FollowSpeed * Time.deltaTime);

                transform.position = new Vector3(transform.position.x, tempvec2.y + YPositionOffset,
                transform.position.z);
            }

            if (!isRotationSetted)
            {
                if(tailAnim != null)
                    tailAnim.enabled = true;
                isRotationSetted = true;
                OnLocationChange();
            }


        }
        //Debug.Log(transform.name + "  " + (targetLocalPosition - transform.localPosition));

    }

    public void StartMovingWithScaling(Vector3 startScale, Vector3 targetScale)
    {
        stackMove.StartMoving(TargetLocalPosition, startScale, targetScale);
    }
}
