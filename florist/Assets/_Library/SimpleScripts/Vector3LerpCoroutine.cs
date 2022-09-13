using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3LerpCoroutine : MonoBehaviour
{

    public delegate bool stopCall();
    public delegate bool JobFinishedCall();
    public delegate void VectorUpdateCall(Vector3 update);
    public delegate Vector3 getSmoothingVectorCall();


   public  static IEnumerator lerpVector(Vector3 start, Vector3 end, float timesecs, stopCall shouldStop, JobFinishedCall finishCall, VectorUpdateCall updateCall, getSmoothingVectorCall smoothingVector)
    {
        Vector3 startIn = start + Vector3.zero;
        Vector3 EndIn = end + Vector3.zero;
        Vector3 resultVector = start + Vector3.zero;

        bool ShouldStop = false;
        Vector3 smoothDamp = smoothingVector.Invoke();

        while ((!ShouldStop && resultVector != EndIn) || (ShouldStop && resultVector == startIn))
        {
            ShouldStop = shouldStop.Invoke();
            if (!ShouldStop)
            {
                resultVector = Vector3.SmoothDamp(resultVector, EndIn, ref smoothDamp , 0.2f);
                if (resultVector == EndIn)
                {
                    finishCall.Invoke();
                    break;
                }

            }
            else
            {
                updateCall.Invoke(resultVector);
                break;
            }
            updateCall.Invoke(resultVector);
            yield return new WaitForEndOfFrame();

        }
        yield return 0;
    }
}
