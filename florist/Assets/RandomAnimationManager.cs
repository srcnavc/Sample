using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationManager : MonoBehaviour
{
    public int result;
    [Tooltip("Min,Max inclusive")]
    [SerializeField] int min = 0;
    [Tooltip("Min,Max inclusive")]
    [SerializeField] int max = 1;
    [SerializeField] Animator anim;
    [SerializeField] string parameterName;
    public void Randomize()
    {
        result = Random.Range(min, max);
        anim.SetInteger(parameterName, result);
    }
}
