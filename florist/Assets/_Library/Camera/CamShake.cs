using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{

    float ShakeElapsedTime;
    static CamShake instance;
    Vector3 shakeFactor;
    // Start is called before the first frame update
    public static Vector3 getShakeOffset()
    {
        if (instance == null)
            return Vector3.zero;
        else 
            return instance.shakeFactor;
    }

    private void Awake()
    {
        instance = this;

    }

    public static void ShakeIt(float dur, float mag)
    {
        if (instance.ShakeElapsedTime == 0)
            instance.StartCoroutine(instance.shakeit(dur, mag));
        else
            instance.ShakeElapsedTime =Mathf.Clamp(instance.ShakeElapsedTime - dur,0,dur);


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
    

}
