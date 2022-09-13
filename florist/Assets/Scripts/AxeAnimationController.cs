using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationController : MonoBehaviour
{
    [SerializeField] string Tag;
    [SerializeField] Animator anim;
    [SerializeField] float hitAnimationSpeedMultiplier = 1;
    const string floatKey = "SpeedMultiplier";
    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    public float HitAnimationSpeedMultiplier { get => Variables.GetFloat("HitAnimationSpeedMultiplier"); }

    public void Animate()
    {
        if(anim.GetFloat(floatKey) != HitAnimationSpeedMultiplier)
            anim.SetFloat(floatKey, HitAnimationSpeedMultiplier);

        anim.SetTrigger("Hit");
    }
}
