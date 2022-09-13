using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ThreeStarsPannel : MonoBehaviour
{
    /*
    public Image[] starImages ;
    
    float[] starValue;
    public float starFillSpeed=1;
    public bool starFillStarted, starFillCompleted;
    public AnimationCurve starJumpCurve;
    ThreeStarsProgressData data;
    int starIndex = 0;
    void Awake()
    {
        GameStateManager.OnGameStateChange += OnGameStateChanged;
    }
    public void checkVisible(GameState state)
    {
        if (state == GameState.fail && !UIMain.getAdapter().getGameSettings().ShowMidPannelOnLoose)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void OnGameStateChanged(GameState state)
    {

      //  Debug.Log("State Event "+state);
        if (state == GameState.win || state == GameState.fail)
        {
            checkVisible( state);

            if (!starFillStarted)
                startStarFill();
            else
            {

                if (!starFillCompleted)
                    starFill();
            }
        }

    }


    public void startStarFill()
    {
      
        data = UIMain.getAdapter().getThreeStartsWinProgress();
        starFillStarted = true;
        starValue = new float[3];
        starIndex = 0; 
    }
    public void starFill()
    {
        float unitIncrease = data.starValues[starIndex] / (starFillSpeed / Time.deltaTime);
        if (starImages[starIndex].fillAmount < 1)
        {
            if (data.TotalProgress > unitIncrease)
            {
                data.TotalProgress -= unitIncrease;
                starValue[starIndex] += unitIncrease;
                if (starValue[starIndex] >= data.starValues[starIndex])
                {
                    data.TotalProgress += starValue[starIndex] - data.starValues[starIndex];
                    starValue[starIndex] = data.starValues[starIndex];
                }
            }
            else
            {
                starValue[starIndex] += data.TotalProgress;
                data.TotalProgress = 0;
                starFillCompleted = true;
            }

            starImages[starIndex].fillAmount = starValue[starIndex] / data.starValues[starIndex];

            if (starImages[starIndex].fillAmount >= 1)
            {
                FXCanvas.explodeStars(starImages[starIndex].transform.position);
                doJump(starImages[starIndex].gameObject);
                starIndex++;
                if(starIndex>2)
                    starFillCompleted = true;
            }
            
            if (data.TotalProgress<=0)
                starFillCompleted = true;
        }

    }

    public void doJump(GameObject star)
    {
        star.AddComponent<SimpleSizeTween>().StartAnimation(5f, .2f, starJumpCurve);
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChanged;
    }

    private void Update()
    {

        if (starFillStarted &&  !starFillCompleted)
            starFill();
    }
    */
}
