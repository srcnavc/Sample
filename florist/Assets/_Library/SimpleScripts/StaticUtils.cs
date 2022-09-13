using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public static class StaticUtils
{
    public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }

    public static String numberToShortSTR(float value)
    {
        String temp;

        if (value < 1000)
            temp = value.ToString("0") ;
        else if (value < 1000000)
        {
            string valueString = (value / 1000).ToString("F");
           if(valueString.Length>5) 
            temp = (value / 1000).ToString("F0") + "K";
           else if(valueString.Length>3)
                temp = (value / 1000).ToString("F1") + "K";
           else
                temp = (value / 1000).ToString("F2") + "K";
        }
        else 
        {
            string valueString = (value / 1000000).ToString("F");
            temp = (value / 1000000).ToString("F" + Mathf.Clamp(5 - valueString.Length, 0, 2)) + "M";
        }
        
        return temp;


    }
    public static Vector3 SetVector3Parameters(Vector3 inVector, float x, float y, float z)
    {

        inVector.x = x;
        inVector.y = y;
        inVector.z = z;
        //  Debug.Log("New Position:"+ inVector.ToString() );
        return inVector;
    }

    public static void bounceGameObject( GameObject ObjectToBounce, GameObject CoroutineSupport, int times, float interval, float scale, float step)
    {

        CoroutineSupport.GetComponent<MonoBehaviour>().StartCoroutine(bounce(ObjectToBounce, times, interval, scale, step));

    }



       static IEnumerator bounce(GameObject go, int times, float interval, float scale, float step)
        {
            Vector3 originalSize = go.transform.localScale;
            for (int i = 0; i < times; i++)
            {
                while (go.transform.localScale.magnitude < originalSize.magnitude * scale)
                {
                    go.transform.localScale *= (1 + step);
                    yield return new WaitForSeconds(interval);

                }

                while (go.transform.localScale.magnitude > originalSize.magnitude)
                {
                    go.transform.localScale /= (1 + step);
                    yield return new WaitForSeconds(interval);
                }
            }
            go.transform.localScale = originalSize;

        }

    public static GameObject FindInChildren(this GameObject go, string name)
    {
        return (from x in go.GetComponentsInChildren<Transform>(true)
                where x.gameObject.name == name
                select x.gameObject).First();
    }


    public static bool IsDegreeBetween(float degree, float min, float max)
    {
        float diffTotal = Mathf.Abs(Mathf.DeltaAngle(min, max));
        float diffWithMin = Mathf.Abs(Mathf.DeltaAngle(degree, min));
        float diffWithMax = Mathf.Abs(Mathf.DeltaAngle(degree, max));
        return diffWithMax <= diffTotal && diffWithMin <= diffTotal;

    }

}
