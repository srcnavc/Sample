using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillImage : MonoBehaviour
{
    public static FillImage ins;
    [SerializeField] Image img;
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }

    public void Activate()
    {
        if(!img.gameObject.activeSelf)
            img.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        if (img.gameObject.activeSelf)
            img.gameObject.SetActive(false);
    }


    public void Fill(float percent)
    {
        img.fillAmount = percent;
    }
}
