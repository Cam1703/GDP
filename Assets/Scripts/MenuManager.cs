using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void Update()
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
        _pauseMenu.SetActive(true);
    }


    public void Unpause()
    {
        PauseManager.instance.UnpauseGame();
        _pauseMenu.SetActive(false);
    }
}
