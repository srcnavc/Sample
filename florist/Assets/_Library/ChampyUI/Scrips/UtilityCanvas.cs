using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UtilityCanvas : CanvasBase
{
    static UtilityCanvas canvas;
    [SerializeField] TextMeshProUGUI Txt_FPS;
    [SerializeField] TextMeshProUGUI Txt_Message;
    string FPSString;
    List<string> messages;
   
    int FPSLastMeasuredValue;
    int _FPS;
    float FPSLastMeasuredTime;
    public int FPS {
        get {
            if (FPSLastMeasuredTime < Time.time-1 )
                {
                FPSLastMeasuredValue = _FPS;
                FPSLastMeasuredValue = (int)((float)FPSLastMeasuredValue / (Time.time - FPSLastMeasuredTime));
                FPSLastMeasuredTime = Time.time;
                _FPS = 0;
                }
            return FPSLastMeasuredValue;
        }
    }

  
    private void Awake()
    {
        messages = new List<string>();
        canvas = this;
    }

    private void LateUpdate()
    {
        UpdateFPS();
    }

    public void UpdateFPS()
    {
        FPSString = "FPS:" + FPS;
        Txt_FPS.text = FPSString;
        _FPS++;
    }


    public static void AddMessage(string message)
    {
        if (canvas == null)
        {
            return;
        }

        canvas.messages.Add(message);
        while (canvas.messages.Count > 3)
            canvas.messages.RemoveAt(0);

        canvas.Txt_Message.text = "";
        for (int i = 0; i < canvas.messages.Count; i++)
        {
            canvas.Txt_Message.text += canvas.messages[i];
            if (i < canvas.messages.Count - 1)
                canvas.Txt_Message.text += "\n";
        }    
    }

    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }



}
