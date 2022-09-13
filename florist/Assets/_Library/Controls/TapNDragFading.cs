using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TapNDragFading : MonoBehaviour,IScreenInput
{

    public float fadeSpeed;
    public float Sensitivity;
    bool touchStarted;
    Vector2 touchStart;
    Vector2 touchEnd;
    Touch touch;
    Vector3 InputPosition;
    float X, Y;
    void Update()
    {
        getInput();
    }

    public void getInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.touches[0];
            fillXY(Input.touchCount > 0, touch.position);
         //   UtilityCanvas.AddMessage("touch:" + X + "," + Y);

        }
        else
        {
            fillXY(Input.GetMouseButton(0), Input.mousePosition);
           // UtilityCanvas.AddMessage("MouseInput:" + X + "," + Y);
        }

    }

    public void fillXY(bool inputComing,Vector3 inPosition)
        {

        if (inputComing)
        {
            InputPosition = inPosition / (Screen.width + Screen.height);
            InputPosition.z = 0;
            if (touchStarted)
            {

                touchEnd = InputPosition;
            }
            else
            {
                touchStart = InputPosition;
                touchEnd = InputPosition;
                touchStarted = true;
            }

        }
        else
        {
            touchStarted = false;
        }

        X = (touchEnd - touchStart).x;
        Y = (touchEnd - touchStart).y;

        touchStart = Vector2.Lerp(touchStart, touchEnd, fadeSpeed * Time.deltaTime);
    }

    public float getX()
    {
        return Sensitivity * X;
    }

    public float getY()
    {
        return Sensitivity * Y;

    }
}
