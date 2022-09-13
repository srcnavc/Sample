using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FieldViewChanger : MonoBehaviour
{
    public static FieldViewChanger ins;
    [SerializeField] Camera cam;
    [SerializeField] float originalView;
    [SerializeField] float closerView;
    [SerializeField] float duration;
    public bool IsEnabled;
    Sequence seq;
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    public void GetCloser()
    {
        cam.DOFieldOfView(closerView, duration);
    }

    public void SetActivity(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }

    private void Disable()
    {
        IsEnabled = false ;
    }
    
    public void ResetAndDisable()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(cam.DOFieldOfView(originalView, duration));
        seq.OnComplete(Disable);

        seq.Play();
        
    }
    public void FarFarAway()
    {
        cam.DOFieldOfView(originalView, duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsEnabled)
            return;

        if (GameStateManager.GetState() != GameState.play)
            return;

        if (Input.GetMouseButtonDown(0))
            GetCloser();

        if (Input.GetMouseButtonUp(0))
            FarFarAway();
    }
}
