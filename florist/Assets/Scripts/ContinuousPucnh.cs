using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousPucnh: MonoBehaviour
{
    [SerializeField] Vector3 startScale;
    [SerializeField] Vector3 targetScale;
    [SerializeField] float duration;
    [SerializeField] int vibration;
    [SerializeField] float elasticty;
    private void Awake()
    {
        startScale = transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartPunch();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            StartPunch();
    }

    private void StartPunch()
    {
        /*Sequence seq = DOTween.Sequence();
        //transform.localScale = startScale;
        seq.Append(transform.DOScale(targetScale, duration));
        
        seq.OnComplete(StartPunch);

        seq.Play();*/
        transform.DOPunchScale(targetScale, duration, vibration, elasticty).SetLoops(-1);
    }
}
