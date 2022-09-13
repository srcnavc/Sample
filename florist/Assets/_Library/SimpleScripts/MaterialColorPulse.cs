using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorPulse : MonoBehaviour
{
    public Material MaterialToPulse;
    public Gradient color;
    public float pulseSpeed;
    float currentState;
    // Update is called once per frame
    private void LateUpdate()
    {

        currentState += Time.deltaTime * pulseSpeed;
        MaterialToPulse.color = color.Evaluate(currentState % 1);

    }
}
