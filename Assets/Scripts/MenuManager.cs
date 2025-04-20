using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _pauseMenu;

    private bool _hasStarted = false;

    private void Start()
    {
        _startMenu.SetActive(true);
        _game.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if ((!_hasStarted) & GameManager.gameManager.playerName != "")
        {
            Begin();
        }
        if (GameManager.gameManager._playerHealth.Health == 0)
        {
            Finish();
        }
        else if (InputManager.menuOpenInput)
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

    public void Begin()
    {
        PauseManager.instance.isPaused = false;
        _hasStarted = true;
        _startMenu.SetActive(false);
        _game.SetActive(true);
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
        SceneManager.LoadScene(2);
    }
}
