using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoom : MonoBehaviour
{
    public void EnableZoom()
    {
        FieldViewChanger.ins.IsEnabled = true;
    }

    public void DisableZoom()
    {
        FieldViewChanger.ins.ResetAndDisable();
    }
}
