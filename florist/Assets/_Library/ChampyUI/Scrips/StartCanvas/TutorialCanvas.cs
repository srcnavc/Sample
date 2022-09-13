using UnityEngine;

public class TutorialCanvas : CanvasBase
{
    void Update()
    {
        checkInput();
    }

    void checkInput()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameStateManager.SetState(GameState.play);
         
        }
    }
}