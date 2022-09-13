using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressWinCanvas : MonoBehaviour
{
    public Image ProgressImage;
    public Image ProgressEmpty;
    public TextMeshProUGUI PercentTxt;
    public float ProgressFillSpeed = 1;
    public bool ProgressFillStarted,  Jumping ;
    public AnimationCurve ProgressJumpCurve;
   /* 
  //  ProgressInfo data; 

    void Awake()
    {
       GameStateManager.OnGameStateChange += OnGameStateChanged;
    }

    public void OnGameStateChanged(GameState state)
    {

      //   Debug.Log("State Event "+state);
        if (state == GameState.win || state == GameState.fail)
        {

            checkVisible(state);

            if (!ProgressFillStarted)
                ProgressFill();
        
        }

    }
    public void checkVisible(GameState state)
    {
        

        if (state == GameState.fail && !UIMain.getAdapter().getGameSettings().ShowMidPannelOnLoose)
        {
            this.gameObject.SetActive(false);
        }
    }



    public void ProgressFill()
    {
    

        data = UIMain.getAdapter().getProgressInfo();

        if (data.alternateImage != null)
        {
            ProgressImage.sprite = data.alternateImage;
            ProgressEmpty.sprite = data.alternateImage;

        }

        ProgressImage.fillAmount = data.curentProgress;
        ProgressFillStarted = true;
    }
    private void Update()
    {
        if (ProgressFillStarted &&!Jumping)
            fillProgress();
    }

    public void fillProgress()
    {
        if (data.curentProgress >= data.finalProgress)
        {
            ProgressFillStarted = false;
            return;
        }

        data.curentProgress += ProgressFillSpeed * Time.deltaTime ;
        data.curentProgress = Mathf.Clamp(data.curentProgress, 0, data.finalProgress);
        ProgressImage.fillAmount = data.curentProgress;
        PercentTxt.text = string.Format("{0:0,0.0} ",  Mathf.Clamp(data.curentProgress * 100,0,100 )) + "%";
        if (data.curentProgress > 1)
        {
            jumpProgresImage();
            data.curentProgress = 0;
            data.finalProgress--;
            data.callback.Invoke();
        }



    }
    public void jumpProgresImage()
    {
        Jumping = true;
        SimpleSizeTween simpleSizeTween = ProgressImage.GetComponent<SimpleSizeTween>();


        if (simpleSizeTween == null)
            simpleSizeTween = ProgressImage.gameObject.AddComponent<SimpleSizeTween>();

        simpleSizeTween.StartAnimation(4, .25f, ProgressJumpCurve, jumpEnds);
    }

    public void jumpEnds(GameObject go)
    {
        Jumping = false;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChanged;
    }
   */
}


