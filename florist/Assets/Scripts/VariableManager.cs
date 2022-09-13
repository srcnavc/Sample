using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableManager : MonoBehaviour
{
    public static VariableManager ins;
    [SerializeField] List<VariableContainer> containers = new List<VariableContainer>();
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    public VariableContainer GetVariableList(string _tag)
    {
        for (int i = 0; i < containers.Count; i++)
        {
            if (containers[i].ListTag == _tag)
                return containers[i];
        }

        throw new System.Exception("There is no Variable List tagged with " + _tag + ".");
    }
}
