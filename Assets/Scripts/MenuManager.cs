using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject _startMenu;
    private GameObject _game;
    private GameObject _pauseMenu;

    public static MenuManager instance;
    public bool _hasStarted = false;

    private void Awake()
    {
        
        if (instance == null && instance != this)
        {
            instance = this;
            Debug.Log(11111111);
        }
    }

    private void Update()
    {
        if (instance._hasStarted)
        {
            Debug.Log(111);
            if (GameManager.instance._playerHealth.Health == 0)
            {
                Finish();
            }
            else if (InputManager.menuOpenInput)
            {
                Debug.Log(111);
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
        if (GameManager.instance.playerName != "")
        {
            PauseManager.instance.UnpauseGame();
            instance._hasStarted = true;
            Debug.Log(instance._hasStarted);
            instance._startMenu.SetActive(false);
            instance._game.SetActive(true);
            Debug.Log(instance._hasStarted);
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
        GameManager.finalName = GameManager.instance.playerName;
        GameManager.finalScore = GameManager.instance.playerScore;
        SceneManager.LoadScene(2);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        instance._startMenu = GameObject.Find("StartContainer");
        instance._pauseMenu = GameObject.Find("PauseContainer");
        instance._game = GameObject.Find("GameProper");

        instance._startMenu.SetActive(true);
        instance._game.SetActive(false);
        instance._pauseMenu.SetActive(false);
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
