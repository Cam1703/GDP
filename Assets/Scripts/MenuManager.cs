using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _finishMenu;

    [SerializeField] private Text _textContainer;

    private bool _hasStarted = false;

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
        _hasStarted = true;
        _startMenu.SetActive(false);
        _game.SetActive(true);
        _textContainer.text = "Player: " + GameManager.gameManager.playerName;

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
