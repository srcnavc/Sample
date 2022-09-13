using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameProgressBar : MonoBehaviour
{
    public Image ProgressFill, ProgressMarker;
    public string parameterString;
    ScriptableAlertingParameterFloat parameter;
    void Start()
    {
        parameter =(ScriptableAlertingParameterFloat) GameManager.Instance.InGameParameters.paramList.findParameter(parameterString);  
        parameter.OnParameterUpdate += updateProgress;
  
        updateProgress();
    }

    
    public void updateProgress()
    {
        ProgressFill.fillAmount = parameter.PValue;
    }
    private void OnDestroy()
    {
        if(parameter)
            parameter.OnParameterUpdate -= updateProgress;
    }

}



//MultipleProgressPointData[] alternateData;
//   public List<Image> AlternateProgressMarkers;
//  static float PROGRESS_MAX = 350, PROGRESS_MIN = -350; 
/*
 

public struct ProgressInfo
{
    public float curentProgress;
    public float finalProgress;

    public delegate void fullCallBack();

    public fullCallBack callback;
    public Sprite alternateImage;

    public ProgressInfo(float curentProgress, float finalProgress, fullCallBack callBack)
    {
        this.curentProgress = curentProgress;
        this.finalProgress = finalProgress;
        this.callback = callBack;
        this.alternateImage = null;
    }

    public ProgressInfo(float curentProgress, float finalProgress, fullCallBack callBack, Sprite alternateImage)
    {
        this.curentProgress = curentProgress;
        this.finalProgress = finalProgress;
        this.callback = callBack;
        this.alternateImage = alternateImage;
    }
}


public void PrepareTheBar()
    {
                ProgressMarker.enabled = false;
                ProgressFill.type = Image.Type.Filled;
                ProgressFill.fillAmount = 0;
                ProgressFill.fillMethod = Image.FillMethod.Horizontal;
                ProgressFill.fillOrigin = (int)Image.OriginHorizontal.Left;
    }

  public void updateProgress()
    {
       switch(UIMain.getAdapter().getGameSettings().progressBarType)
        {
            case ProgressBarType.Fill:
                ProgressFill.fillAmount = parameter.PValue;

                break;
            case ProgressBarType.Point:
                 Temp_Vec2 = ProgressMarker.rectTransform.localPosition;
                 Temp_Vec2.x = Mathf.Lerp(PROGRESS_MIN, PROGRESS_MAX, Adapter.getInGameParameters().inGameProgress);
                 ProgressMarker.rectTransform.localPosition = Temp_Vec2;

                break;
            case ProgressBarType.Both:
                 Temp_Vec2 = ProgressMarker.rectTransform.localPosition;
                 Temp_Vec2.x = Mathf.Lerp(PROGRESS_MIN, PROGRESS_MAX, Adapter.getInGameParameters().inGameProgress);
                 ProgressMarker.rectTransform.localPosition = Temp_Vec2;
                 ProgressFill.fillAmount = Adapter.getInGameParameters().inGameProgress;
                break;

            case ProgressBarType.Multiple:
                alternateData = Adapter.getMultipleProgress();
                for (int i = 0; i < alternateData.Length; i++)
                {
                    if (i == 0)
                    {
                        Temp_Vec2 = ProgressMarker.rectTransform.localPosition;
                        Temp_Vec2.x = Mathf.Lerp(PROGRESS_MIN, PROGRESS_MAX, alternateData[i].position);
                        ProgressMarker.rectTransform.localPosition = Temp_Vec2;
                        ProgressMarker.color = alternateData[i].color;
                    }
                    else
                    {
                        Temp_Vec2 = AlternateProgressMarkers[i - 1].rectTransform.localPosition;
                        Temp_Vec2.x = Mathf.Lerp(PROGRESS_MIN, PROGRESS_MAX, alternateData[i].position);
                        AlternateProgressMarkers[i - 1].rectTransform.localPosition = Temp_Vec2;
                        AlternateProgressMarkers[i - 1].color = alternateData[i].color;
                    }

                }
                
                break;

        }
      

    }



 public void PrepareTheBar()
    {
        switch (UIMain.getAdapter().getGameSettings().progressBarType)
        {
            case ProgressBarType.Fill:
                ProgressMarker.enabled = false;
                ProgressFill.type = Image.Type.Filled;
                ProgressFill.fillAmount = 0;
                ProgressFill.fillMethod = Image.FillMethod.Horizontal;
                ProgressFill.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;
            case ProgressBarType.Point:
                ProgressMarker.enabled = true;
                ProgressFill.type = Image.Type.Simple;
                break;
            case ProgressBarType.Both:
                ProgressMarker.enabled = true;
                ProgressFill.type = Image.Type.Filled;
                ProgressFill.fillAmount = 0;
                ProgressFill.fillMethod = Image.FillMethod.Horizontal;
                ProgressFill.fillOrigin = (int)Image.OriginHorizontal.Left;
                break;
            case ProgressBarType.Multiple:
                AlternateProgressMarkers = new List<Image>();
                alternateData = Adapter.getMultipleProgress();
                for (int i = 1; i < alternateData.Length; i++)
                {
                      Image temp = Instantiate(ProgressMarker, ProgressMarker.transform.parent);
                      temp.transform.position = ProgressMarker.transform.position + (Vector3)(Vector2.up * alternateData[i].Offsety);
                      AlternateProgressMarkers.Add(temp);
                  }
                Transform myParent = ProgressMarker.transform.parent;
                ProgressMarker.transform.SetParent(null);
                ProgressMarker.transform.SetParent(myParent);
                ProgressMarker.enabled = true;
                ProgressFill.type = Image.Type.Simple;
                break;


        }
    }

 */



