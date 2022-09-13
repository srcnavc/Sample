using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour,IObjectFollower
{
    public GameObject ToFollow;
   // public GameObject RemoteControll;
    public float followSmoothing =1;
    public Vector3 offset , smoothing;
    public Vector3 originalPos,currentPos;
    public bool autoSetOffset = true;
    public bool lookAt;
    Quaternion lookAtState,oldRotation,lookAtTarget;

 

   // float ShakeElapsedTime;
    //Vector3 shakeFactor;

   
    void Awake()
    {
        currentPos = transform.position;
        originalPos = transform.position;
        if (ToFollow != null&& autoSetOffset)
        {
            setOffset(ToFollow.transform);
        }
        lookAtState = transform.rotation;
    }

    public void SetToFollow(Transform follow)
    {
        ToFollow = follow.gameObject;
        setOffset(follow);
    }
    public void setOffset(Transform follow)
    {
        offset = originalPos - follow.transform.position;
        currentPos = transform.position;
    }

    public void setOffset(Vector3 overrideOffset)
    {
        offset = overrideOffset ;
      
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (ToFollow != null)
        {
            currentPos = Vector3.SmoothDamp(currentPos, ToFollow.transform.position + offset, ref smoothing, followSmoothing);
            transform.position = currentPos+CamShake.getShakeOffset() ;
           
            if (lookAt)
            {
                oldRotation = transform.rotation;
                transform.LookAt(ToFollow.transform);
                
                lookAtTarget = transform.rotation;
                transform.rotation = Quaternion.Slerp(oldRotation, lookAtTarget, 0.02f);

            }
        }

    }

    public void setFollowObject(GameObject toFollow)
    {
        SetToFollow(toFollow.transform);
        offset = Vector3.zero;
    }
    /*

public void ShakeIt(float dur, float mag)
{
   if(ShakeElapsedTime==0)
     StartCoroutine(shakeit(dur, mag));


}

IEnumerator shakeit(float duration, float magnitude)
{

   ShakeElapsedTime = 0;

   while (ShakeElapsedTime < duration)
   {
       shakeFactor = Vector3.zero;

       ShakeElapsedTime += Time.deltaTime;


       float x = Random.Range(-1f, 1f) * magnitude * Time.timeScale;
       float y = Random.Range(-1f, 1f) * magnitude * Time.timeScale;
       // float z = Random.Range(-1f, 1f) * magnitude;


       shakeFactor.x = x;
       shakeFactor.y = y;

       yield return null;
   }

   shakeFactor = Vector3.zero;
   duration = 0;
   ShakeElapsedTime = 0;

}
*/

}
