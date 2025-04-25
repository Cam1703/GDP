using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject _startMenu;
    private GameObject _game;
    private GameObject _pauseMenu;

    public static MenuManager instance;
    private bool _hasStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
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
        Debug.Log("22222");
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

    public void OnPlayButton()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
        Debug.Log(0);
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
    {
        Debug.Log(1);
        yield return new WaitForSecondsRealtime(0.1f);
        Debug.Log(2);
        _startMenu = GameObject.Find("StartContainer");
        Debug.Log(_startMenu);
        _pauseMenu = GameObject.Find("PauseContainer");
        _game = GameObject.Find("GameProper");

        _startMenu.SetActive(true);
        _game.SetActive(false);
        _pauseMenu.SetActive(false);
        Destroy(this.gameObject);
    }
    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

}
