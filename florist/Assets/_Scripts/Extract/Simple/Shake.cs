using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour,IOffsetEffector
{
    float ShakeElapsedTime;
    Vector3 shakeFactor;


    public void ShakeIt(float dur, float mag)
    {
        if (ShakeElapsedTime == 0)
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

    public Vector3 getCurrentOffset()
    {
        return shakeFactor;
    }
}
