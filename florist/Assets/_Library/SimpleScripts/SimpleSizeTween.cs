using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSizeTween : MonoBehaviour
{
    [SerializeField]AnimationCurve curve;
    Vector3 originalScale;
    bool IsStarted;
    [SerializeField] float speed;
    public float duration;
    public delegate void CallBack(GameObject gObject);
    CallBack callBack;
    float startTime;
    public bool returnToOriginal;

    void Start()
    {
        setOriginal();
    }
    public void setOriginal()
    {
        originalScale = transform.localScale;
    }

    public void StartAnimation(float speed, float duration, AnimationCurve InCurve, CallBack callBack)
    {
        this.callBack = callBack;
        curve = InCurve;
        StartAnimation(speed, duration, InCurve);
    }

    public void StartAnimation(float speed, float duration,AnimationCurve InCurve)
    {   if (InCurve != null)
            this.curve = InCurve;
        StartAnimation(speed, duration);
    }

    public void StartAnimation(float speed,float duration)
    {
        this.speed = speed;
        this.duration = duration;
        this.speed = 1f / duration;
        startTime = Time.time;
        IsStarted = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsStarted)
        {
            transform.localScale = curve.Evaluate((Mathf.Clamp(Time.time- startTime, 0,duration) * speed) ) * originalScale;

            if (startTime + duration < Time.time)
            {
                IsStarted = false;
                if(returnToOriginal)
                    transform.localScale = originalScale;
                if (callBack != null)
                    callBack.Invoke(gameObject);

            }


        }
        
    }
}
