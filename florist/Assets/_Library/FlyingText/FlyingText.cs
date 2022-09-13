using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FlyingText : MonoBehaviour,IPoolObject
{
    [SerializeField] TextMeshProUGUI Text;
    [SerializeField] Vector3 startOffset,endOffset;
    [SerializeField] float duration;
    [SerializeField] AnimationCurve SizeCurve, OpacityCurve;

    Vector3 originalSize;
    Vector3 originalPosition;
    float startTime,progress;
    Color OriginalColor,tempColor;


    public void StartText(Vector3 position, string text)
    {
        StartText(position, text, Text.color);
    }
    public void StartText(Vector3 position,string text, Color textColor )
    {
        OriginalColor = textColor;
        Text.text = text;
        startTime = Time.time;
        originalPosition = position;

    }
    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;
    }


    void Update()
    {
        if (startTime != -1)
        {
            DoAnimate();
        }
    }

    private void DoAnimate()
    {
        if (startTime + duration > Time.time)
        {
            tempColor = OriginalColor;
            progress = (Time.time - startTime) / duration;
            transform.localScale = originalSize * SizeCurve.Evaluate(progress);
            transform.position = originalPosition + Vector3.Lerp(startOffset, endOffset, progress);
            tempColor.a = OriginalColor.a * OpacityCurve.Evaluate(progress);
            Text.color = tempColor;
        }
        else
        {
            startTime = -1;
            if (GetComponent<PoolObject>() != null)
                GetComponent<PoolObject>().release();
            
        }
    }

    public void clearForRelease()
    {
        transform.parent = null;
        transform.localScale = originalSize;
        Text.color = OriginalColor;
    }

    public void resetForRotate()
    {
        transform.parent = null;
        transform.localScale = originalSize;
        Text.color = OriginalColor;
    }

    public void UpdateText(string text , Color color)
    {
        startTime = Time.time;
        Text.text = text;
        Text.color = color;

    }

    public void UpdateText(Vector3 position , string text, Color color)
    {
        startTime = Time.time;
        originalPosition = position;
        Text.color = color;
        Text.text = text;
    }

    public void OnCreate()
    {
      //  throw new NotImplementedException();
    }
}
