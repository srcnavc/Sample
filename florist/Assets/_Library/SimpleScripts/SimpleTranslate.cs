using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTranslate : MonoBehaviour
{
    public Vector3 targetPos;
    public float translate_time;
    public Vector3 smoothDampRef;
    bool started;
    public bool removeScriptAtFinish;
    public delegate void  CallBack(GameObject obj);
    public CallBack callBack;
    public float sensitivity;

    public void Translate(float time,Vector3 target,bool removeAtFinish , CallBack callback )
    {

        sensitivity = Vector3.Distance(target, transform.position) / 100;
        translate_time = time;
        targetPos = target;
        started = true;
        removeScriptAtFinish = removeAtFinish;
        callBack = callback;

    }
    // Update is called once per frame
    void Update()
    {   if (started)
        {
            if (Vector3.Distance(transform.position, targetPos) > sensitivity)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref smoothDampRef, translate_time);
            }
            else
            {
                started = false;
                if (removeScriptAtFinish)
                {
                    gameObject.SetActive(false);
                    if (GetComponent<PoolObject>() != null)
                        GetComponent<PoolObject>().release();
                }

                if (callBack != null)
                    callBack.Invoke(gameObject);

            }
        }
    }
}
