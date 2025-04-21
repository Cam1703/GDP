using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _pauseMenu;

    public static MenuManager instance;
    private bool _hasStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _startMenu.SetActive(true);
        _game.SetActive(false);
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (_hasStarted)
        {
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
    }

    public void Begin()
    {
        if (GameManager.gameManager.playerName != "")
        {
            PauseManager.instance.UnpauseGame();
            _hasStarted = true;
            _startMenu.SetActive(false);
            _game.SetActive(true);
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
        GameManager.finalName = GameManager.gameManager.playerName;
        GameManager.finalScore = GameManager.gameManager.playerScore;
        SceneManager.LoadScene(2);
    }
}
