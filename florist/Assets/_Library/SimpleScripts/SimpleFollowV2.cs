using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollowV2 : MonoBehaviour,IObjectFollower
{
    public GameObject ToFollow;
    List<IOffsetEffector> offsetEffectors; 
    [Range(0f,10f)] public float PositionFollowSpeed;
    [Range(0f, 10f)] public float RotationFollowSpeed;
    public bool offsetModifiers;

    private void Start()
    {
        offsetEffectors = new List<IOffsetEffector>();
        offsetEffectors.AddRange(GetComponents<IOffsetEffector>());

    }
    public void SetToFollow(Transform follow)
    {
        ToFollow = follow.gameObject;
    }
   void LerpPosition()
    {
        transform.position = Vector3.Lerp(transform.position, ToFollow.transform.position, PositionFollowSpeed * Time.deltaTime);
    }

    void LerpRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, ToFollow.transform.rotation, RotationFollowSpeed * Time.deltaTime);
    }

   
    void LateUpdate()
    {
        if (ToFollow != null)
        {
            LerpPosition();
            LerpRotation();
            if (offsetModifiers)
            {
                addOffsets();
            }
        }

    }

    public void setFollowObject(GameObject toFollow)
    {
        SetToFollow(toFollow.transform);
    }
    private void OnEnable()
    {
    //    Debug.Log(name + ":Enabled");
    }
    public void addOffsets()
    {
        
        foreach (IOffsetEffector item in offsetEffectors)
        {
            transform.position += item.getCurrentOffset();
        }
    
    }

}
