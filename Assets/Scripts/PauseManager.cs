using UnityEditorInternal;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    public static PauseManager instance;

    public bool isPaused;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        InputManager.playerInput.SwitchCurrentActionMap("UI");
    }

    public void UnpauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        InputManager.playerInput.SwitchCurrentActionMap("Player");
    }

    public void FinishGame()
    {
        PauseGame();
        InputManager.playerInput.SwitchCurrentActionMap("ExitMenu");
    }
}
