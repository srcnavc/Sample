using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoPunchScale : MonoBehaviour
{
    [SerializeField] Vector3 endValue;
    [SerializeField] float duration;
    [SerializeField] int vibration;
    [SerializeField] float elasticty;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            PunchIt();
    }
    public void PunchIt()
    {
        transform.DOPunchScale(1.2f * Vector3.one, duration, vibration, elasticty);
    }
}
