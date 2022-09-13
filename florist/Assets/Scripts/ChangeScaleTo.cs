using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleTo : MonoBehaviour
{
    [Tooltip("If target transform is null, attached GameObject is the target transform.")]
    [SerializeField] Transform targetTransform;
    [SerializeField] float scaleMultiplier;
    [SerializeField] float Duration;
    [SerializeField] Vector3 originalScale;
    float timer;
    bool start = false;
    Vector3 startScale;
    Vector3 targetScale;
    
    private void Awake()
    {
        if(targetTransform == null)
            targetTransform = transform;
        
        //originalScale = targetTransform.localScale;
    }
    public void GetBigger()
    {
        timer = 0f;
        startScale = targetTransform.localScale;
        targetScale += (originalScale * scaleMultiplier);
        start = true;
    }

    public void ResetScale()
    {
        timer = 0f;
        startScale = targetTransform.localScale;
        targetScale = originalScale;
        start = true;
    }

    public void GetSmaller()
    {
        if(scaleMultiplier <= 1f && scaleMultiplier > 0f)
        {
            timer = 0f;
            startScale = targetTransform.localScale;
            targetScale -= (originalScale * scaleMultiplier);
            start = true;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;

            targetTransform.localScale = Vector3.Lerp(startScale, targetScale, timer / Duration);
            
            if (timer / Duration >= 1f)
            {
                start = false;
                targetTransform.localScale = targetScale;
            }
        }
    }
}
