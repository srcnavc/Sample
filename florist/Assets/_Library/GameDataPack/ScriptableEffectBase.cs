using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableEffectBase : ScriptableObject
{
    public float strength;
    public float duration;
    public int stepCount;
    public EffectExecutionModel executionModel;
    public delegate void endEffectCallBack();
    public float starttime;
    endEffectCallBack callback;
    public void doEffect(GameObject effectMaster, endEffectCallBack callback )
    {
        this.callback = callback;
        starttime = Time.time;
        StartEffect(effectMaster.gameObject);
        if (duration > 0)
            effectMaster.GetComponent<MonoBehaviour>().StartCoroutine(endEffect(effectMaster));
    }
    public abstract void StartEffect(GameObject Effected);

    public abstract void StepEffect(GameObject Effected, int step);
    public abstract void EndEffect(GameObject Effected);

     public IEnumerator endEffect(GameObject Effected)
    {
        if (executionModel == EffectExecutionModel.StepByStep)
        {
            for (int i = 1; i <= stepCount; i++)
            {

                yield return new WaitForSeconds(duration / stepCount);
                StepEffect(Effected, i);
            }
        }
        else if (executionModel == EffectExecutionModel.EveryFrameStep)
        { 
        
            while(starttime+duration>Time.time)
            {

                yield return new WaitForEndOfFrame();
                StepEffect(Effected, 0);
            }

        }


        EndEffect(Effected);
        callback.Invoke();

    }

 }
public enum EffectExecutionModel
{
StepByStep = 1,
EveryFrameStep =  2
}
