using UnityEngine;

public class MenuManager : MonoBehaviour
{

    
    void Update()
    {
        if (InputManager.menuOpenInput)
        {
            if (!PauseManager.instance.isPaused)
            {
                Pause();
            }
        }
        else if (InputManager.menuCloseInput)
        {
            Unpause();
        }
    }

    public void Pause() 
    {
        PauseManager.instance.PauseGame();
    }


    public void Unpause()
    {
        PauseManager.instance.UnpauseGame();
    }
}
