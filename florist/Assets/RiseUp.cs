using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class RiseUp : MonoBehaviour
{
    public UnityEvent OnStart;
    public UnityEvent OnFinish;
    [SerializeField] GameObject objectToRise;
    [SerializeField] Collider col;
    [SerializeField] float riseAmount;
    [SerializeField] float riseDuration;

    [SerializeField] Vector3 targetDestination;
    Sequence seq;
    float originalHeight;
    bool isFirst = true;

    private void Start()
    {
        originalHeight = objectToRise.transform.position.y;
    }
    public void Rise()
    {
        col.enabled = false;
        OnStart?.Invoke();
        targetDestination = objectToRise.transform.position;
        targetDestination.y += riseAmount;
        
        seq = DOTween.Sequence();
        seq.Append(objectToRise.transform.DOMove(targetDestination, riseDuration));
        seq.OnComplete(BuildNavMesh);

        seq.Play();
    }

    public void ResetRoad()
    {
        //objectToRise.transform.position.y = originalHeight;
    }
    private void BuildNavMesh()
    {
        InfiniteRoad.ins.BuildNavmesh();
        OnFinish?.Invoke();
    }
}
