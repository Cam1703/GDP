using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _finishMenu;

    private void Start()
    {
        _pauseMenu.SetActive(false);
        _finishMenu.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.gameManager._playerHealth.Health == 0) {
            Finish();
        }
        else  if (InputManager.menuOpenInput)
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

    public void Finish()
    {
        PauseManager.instance.FinishGame();
        _pauseMenu.SetActive(false);
        _finishMenu.SetActive(true);
    }
}
