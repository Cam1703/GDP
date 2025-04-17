using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _finishMenu;

    private bool _hasStarted = false;
    private bool _hasFinished = false;

    private void Start()
    {
        _startMenu.SetActive(true);
        _game.SetActive(false);
        _pauseMenu.SetActive(false);
        _finishMenu.SetActive(false);
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
        PauseManager.instance.FinishGame();
        _pauseMenu.SetActive(false);
        _game.SetActive(false);
        _finishMenu.SetActive(true);
        if (!_hasFinished)
        {
            LeaderboardManager.instance.Leaderboard();
            _hasFinished = true;
        }
    }
}
