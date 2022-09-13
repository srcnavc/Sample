using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.OnPlayerStateChanged += OnPlayerStateChanged;
        FrontStackUp.ins.OnStackCountChanged += OnStackCountChanged;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerStateChanged -= OnPlayerStateChanged;
        FrontStackUp.ins.OnStackCountChanged -= OnStackCountChanged;
    }

    private void OnStackCountChanged(int count)
    {
        if(count <= 0)
            SetBool("HavePot", false);
        else
            SetBool("HavePot", true);
    }

    private void OnPlayerStateChanged(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                SetBool("Running", false);
                break;
            case PlayerState.Run:
                SetBool("Running", true);
                break;
            default:
                break;
        }
    }

    private void SetBool(string name, bool value)
    {
        if (anim.GetBool(name) != value)
            anim.SetBool(name, value);
    }
}
