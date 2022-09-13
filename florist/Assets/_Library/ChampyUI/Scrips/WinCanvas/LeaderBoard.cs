using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderBoard : MonoBehaviour
{
   /* public TextMeshProUGUI[] users;
    LeaderBoardRecord[] data;
    
    void Awake()
    {
       GameStateManager.OnGameStateChange += OnGameStateChanged;
    }

    public void OnGameStateChanged(GameState state)
    {

        

        if (state == GameState.win|| state == GameState.fail)
        {
            checkVisible(state);
          

            data = UIMain.getAdapter().getLeaderBoard();
            for (int i = 0; i < data.Length && i < users.Length; i++)
            {
                users[i].text = data[i].SortId + "." + data[i].UserName;
                users[i].color = data[i].displayColor;
            }
        }
    }

  

    public void checkVisible(GameState state)
    {
        if (state == GameState.fail && !UIMain.getAdapter().getGameSettings().ShowMidPannelOnLoose)
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= OnGameStateChanged;
    }
   */
}



